using CSharpFunctionalExtensions;
using EShop.Application.Dtos.User;
using EShop.Application.Features.Commands.Users.Create;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost("create")]
        public async Task<ActionResult<Result<Guid>>> Create(
            [FromBody] CreateUserDto createUserDto,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(createUserDto.Adapt<CreateUserCommand>(), cancellationToken);

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status201Created, result.Value)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Error);
        }
    }
}
