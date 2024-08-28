using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using EShop.Application.Features.Commands.Users.Create;
using EShop.Application.Features.Commands.Users.Delete;
using EShop.Application.Features.Commands.Users.Update;
using EShop.Application.Features.Queries.Users.ById;
using EShop.Application.Features.Queries.Users.List;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpGet("list")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IEnumerable<UsersListItemDto>> GetList(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUsersListItemQuery(), cancellationToken);
    }


    [HttpPost("create")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Result<Guid>>> Create(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createUserDto.Adapt<CreateUserCommand>(), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpPatch("update-main-info")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<bool>>> UpdateMainInfo(
        [FromBody] UpdateUserDto updateUserDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(updateUserDto.Adapt<UpdateUserCommand>(), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Administrator, RegisteredUser")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id), cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status200OK, result.Value)
            : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
    }
}
