using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;

namespace EShop.Web.Controllers;

public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Result<UserDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpGet("list")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IEnumerable<UsersListItemDto>> GetList(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUsersListItemQuery(), cancellationToken);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Guid>>> Create(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createUserDto.Adapt<CreateUserCommand>(), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : NotFound(result.Error);
    }


    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager, RegisteredUser")]
    public async Task<ActionResult<Result>> Update(
        Guid id,
        [FromBody] UpdateUserDto updateUserDto,
        CancellationToken cancellationToken)
    {
        updateUserDto.Id = id;

        var result = await Mediator.Send(updateUserDto.Adapt<UpdateUserCommand>(), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager, RegisteredUser")]
    public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id), cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }
}
