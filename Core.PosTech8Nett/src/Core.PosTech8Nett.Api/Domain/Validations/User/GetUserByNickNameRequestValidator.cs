using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using FluentValidation;

namespace Core.PosTech8Nett.Api.Domain.Validations.User
{
    public class GetUserByNickNameRequestValidator : AbstractValidator<GetUserByNickNameRequest>
    {
        public GetUserByNickNameRequestValidator()
        {
            RuleFor(x => x.NickName)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo NickName deve ser informado.");
        }
    }
}
