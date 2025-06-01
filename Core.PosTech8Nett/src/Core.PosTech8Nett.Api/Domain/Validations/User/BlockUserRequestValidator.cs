using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using FluentValidation;

namespace Core.PosTech8Nett.Api.Domain.Validations.User
{
    public class BlockUserRequestValidator : AbstractValidator<BlockUserRequest>
    {
        public BlockUserRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("O campo Id deve ser informado.");
        }
    }
}