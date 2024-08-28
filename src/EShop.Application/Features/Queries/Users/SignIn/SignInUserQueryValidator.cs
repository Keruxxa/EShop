using FluentValidation;

namespace EShop.Application.Features.Queries.Users.SignIn;

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

