using EShop.Application.Dtos.User;
using EShop.Application.Features.Commands.Users.SignIn;
using EShop.Infrastructure.Services.Auth;
using EShop.Infrastructure.Utilities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EShop.Web.Controllers;

public class AuthenticationController : BaseController
{
    private readonly IOptions<JwtOptions> _options;

    public AuthenticationController(IMediator mediator, IOptions<JwtOptions> options) : base(mediator)
    {
        _options = options;
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<Guid>> SignUn([FromBody] SignUpUserDto signUpUserDto)
    {
        var result = await Mediator.Send(signUpUserDto.Adapt<SignUpUserCommand>());

        if (result.IsFailure)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }

        var user = result.Value;

        var token = new JwtTokenService(_options).Generate(user);

        HttpContext.Response.Cookies.Append("eshop-server-cookies", token);

        return Ok(token);
    }
}
