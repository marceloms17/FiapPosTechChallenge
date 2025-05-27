using Core.PosTech8Nett.Api.Domain.Model.User.Requests;
using FluentValidation;
using System;

namespace Core.PosTech8Nett.Api.Domain.Validations.User
{
    public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
    {
        public GetUserByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("O campo Id deve ser informado.");
        }
    }
}