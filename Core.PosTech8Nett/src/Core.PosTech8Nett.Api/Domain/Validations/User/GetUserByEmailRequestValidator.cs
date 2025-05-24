using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using FluentValidation;

namespace Core.PosTech8Nett.Api.Domain.Validations.User
{
    public class GetUserByEmailRequestValidator : AbstractValidator<GetUserByEmailRequest>
    {
        public GetUserByEmailRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("O campo Email deve conter um endereço de e-mail válido.");
        }
    }
}
