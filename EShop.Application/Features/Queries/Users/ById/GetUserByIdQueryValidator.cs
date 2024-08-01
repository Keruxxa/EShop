using FluentValidation;

namespace EShop.Application.Features.Queries.Users.ById
{
    /// <summary>
    ///     Представялет валидотор запроса <see cref="GetUserByIdQuery"/>
    /// </summary>
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
            => RuleFor(query => query.Id).NotEmpty();
    }
}
