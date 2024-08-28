using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
