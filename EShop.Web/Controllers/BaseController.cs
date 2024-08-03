using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator;

    internal Guid UserId => User.Identity.IsAuthenticated
        ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
        : Guid.Empty;


    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
