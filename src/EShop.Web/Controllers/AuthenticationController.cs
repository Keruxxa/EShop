using CSharpFunctionalExtensions;
using EShop.Application.CQRS.Commands.RefreshTokens;
using EShop.Application.CQRS.Commands.Users;
using EShop.Application.CQRS.Queries.Users;
using EShop.Application.Dtos.Auth;
using EShop.Application.Dtos.User;
using EShop.Application.Issues.Errors.Base;
using EShop.Infrastructure.Utilities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EShop.Web.Controllers;

public class AuthenticationController(IMediator mediator, IOptions<JwtOptions> options) : BaseController(mediator)
{
    private readonly JwtOptions _options = options.Value;

    private const string REFRESH_TOKEN_KEY = "refresh-token";
    private const string NO_REFRESH_TOKEN_PROVIDED = "No refresh token provided";

    [HttpPost("sign-up")]
    public async Task<ActionResult<Result<SignUpUserResponseDto, Error>>> SignUp([FromBody] SignUpUserDto signUpUserDto, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(signUpUserDto.Adapt<SignUpUserCommand>(), cancellationToken);

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

        var response = result.Value;

        Response.Cookies.Append(REFRESH_TOKEN_KEY, response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiresDays)
        });

        return Ok(response);
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<SignInUserResponseDto, Error>>> SignIn([FromBody] SignInUserDto signInUserDto, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(signInUserDto.Adapt<SignInUserQuery>(), cancellationToken);

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

        var response = result.Value;

        Response.Cookies.Append(REFRESH_TOKEN_KEY, response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/",
            Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiresDays)
        });

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<Result<RefreshTokenResponse, Error>>> RefreshToken(CancellationToken cancellationToken)
    {
        if (!Request.Cookies.TryGetValue(REFRESH_TOKEN_KEY, out var refreshToken))
        {
            return Unauthorized(NO_REFRESH_TOKEN_PROVIDED);
        }

        var result = await Mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error.EntityError.Message);
        }

        var response = result.Value;

        Response.Cookies.Append(REFRESH_TOKEN_KEY, response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiresDays)
        });

        return Ok(response);
    }

    [HttpPost("sign-out")]
    public async Task<ActionResult<Result<Unit, Error>>> SignOut(CancellationToken cancellationToken)
    {
        if (Request.Cookies.TryGetValue(REFRESH_TOKEN_KEY, out var refreshToken))
        {
            await Mediator.Send(new SignOutCommand(refreshToken), cancellationToken);
        }

        Response.Cookies.Delete(REFRESH_TOKEN_KEY);

        return NoContent();
    }
}
