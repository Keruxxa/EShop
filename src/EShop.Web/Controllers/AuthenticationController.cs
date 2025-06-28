using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.User;
using EShop.Application.Issues.Errors.Base;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

public class AuthenticationController : BaseController
{
    public AuthenticationController(IMediator mediator) : base(mediator)
    {
    }


    [HttpPost("sign-up")]
    public async Task<ActionResult<Result<SignUpUserResponseDto, Error>>> SignUp([FromBody] SignUpUserDto signUpUserDto)
    {
        var result = await Mediator.Send(signUpUserDto.Adapt<SignUpUserCommand>());

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.ErrorType switch
            {
                ErrorType.Duplicate => Conflict(error),
                ErrorType.ServerError => StatusCode(StatusCodes.Status500InternalServerError, error),
                _ => BadRequest(error)
            };
        }

        var userResponseDto = result.Value;

        return Ok(userResponseDto);
    }


    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<SignInUserResponseDto, Error>>> SignIn([FromBody] SignInUserDto signInUserDto)
    {
        var result = await Mediator.Send(signInUserDto.Adapt<SignInUserQuery>());

        if (result.IsFailure)
        {
            var error = result.Error;

            return error.ErrorType switch
            {
                ErrorType.NotFound => NotFound(error),
                ErrorType.BadRequest => BadRequest(error),
                _ => BadRequest(error)
            };
        }

        var userResponseDto = result.Value;

        return Ok(userResponseDto);
    }
}
