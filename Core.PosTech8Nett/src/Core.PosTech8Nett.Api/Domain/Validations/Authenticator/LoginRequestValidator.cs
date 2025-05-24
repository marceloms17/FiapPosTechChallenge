using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using FluentValidation;

namespace Core.PosTech8Nett.Api.Domain.Validations.Authenticator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().NotNull()
            .WithMessage("O campo UserName é obrigatório.");

            RuleFor(x => x.Password)
             .NotEmpty().NotNull()
             .WithMessage("O campo Password é obrigatório.");
        }
    }
}
