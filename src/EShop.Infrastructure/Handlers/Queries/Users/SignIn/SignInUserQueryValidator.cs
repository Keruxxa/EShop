using EShop.Application.CQRS.Queries.Users;
using FluentValidation;

namespace EShop.Infrastructure.Handlers.Queries.Users.SignIn;

/// <summary>
///     Преставялет валидатор запроса <see cref="SignInUserQuery"/>
/// </summary>
public class SignInUserQueryValidator : AbstractValidator<SignInUserQuery>
{
    public SignInUserQueryValidator()
    {
        RuleFor(command => command.Email).MaximumLength(64);

        RuleFor(command => command.Password).MaximumLength(64);
    }
}
