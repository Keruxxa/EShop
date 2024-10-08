using EShop.Application.Dtos.User;
using EShop.Application.Interfaces.Security;
using EShop.Infrastructure.Utilities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Issues.Errors.Base;
using static EShop.Application.Constants;
using CSharpFunctionalExtensions;

namespace EShop.Web.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationController(
        IMediator mediator,
        IOptions<JwtOptions> options,
        IJwtTokenService jwtTokenService) : base(mediator)
    {
        _options = options;
        _jwtTokenService = jwtTokenService;
    }


    [HttpPost("sign-up")]
    public async Task<ActionResult<SignUpUserResponseDto>> SignUp([FromBody] SignUpUserDto signUpUserDto)
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

        var user = result.Value;

        var token = _jwtTokenService.Generate(user);

        HttpContext.Response.Cookies.Append(ESHOP_SERVER_COOKIES, token);

        return Ok(new SignUpUserResponseDto(user.Id, token));
    }


    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<SignInUserResponseDto, Error>>> SignIn([FromBody] SignInUserDto signInUserDto)
    {
        if (User.Identity.IsAuthenticated)
        {
            return BadRequest(USER_IS_ALREADY_AUTHENTICATED);
        }

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

        var user = result.Value;

        var token = _jwtTokenService.Generate(user);

        HttpContext.Response.Cookies.Append(ESHOP_SERVER_COOKIES, token);

        return Ok(new SignInUserResponseDto(user.Id, token));
    }


    [HttpDelete("{id:Guid}")]
    public ActionResult SignOut(Guid id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return BadRequest($"User with id '{id}' is not authenticated");
        }

        HttpContext.Response.Cookies.Delete(ESHOP_SERVER_COOKIES);

        return NoContent();
    }
}
