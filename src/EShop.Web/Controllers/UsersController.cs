using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Issues.Errors.Base;

namespace EShop.Web.Controllers;

public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }


    [HttpGet("list")]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<IEnumerable<UsersListItemDto>> GetList(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUsersListItemQuery(), cancellationToken);
    }


    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Result<UserDto, Error>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            _ => BadRequest(error)
        };
    }


    [HttpPost]
    [Authorize(Roles = "Administrator, Manager")]
    public async Task<ActionResult<Result<Guid, Error>>> Create(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createUserDto.Adapt<CreateUserCommand>(), cancellationToken);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.Duplicate => Conflict(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }


    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager, RegisteredUser")]
    public async Task<ActionResult<Result<Unit, Error>>> Update(
        Guid id,
        [FromBody] UpdateUserDto updateUserDto,
        CancellationToken cancellationToken)
    {
        updateUserDto.Id = id;

        var result = await Mediator.Send(updateUserDto.Adapt<UpdateUserCommand>(), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }


    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Administrator, Manager, RegisteredUser")]
    public async Task<ActionResult<Result<Unit, Error>>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteUserCommand(id), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        var error = result.Error;

        return error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(error),
            ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
            _ => BadRequest(error)
        };
    }
}
