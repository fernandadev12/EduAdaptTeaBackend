using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AdaptaEdu.Application.Validation
{
    public class CriarProfessorRequestValidator : AbstractValidator<CriarProfessorRequest>
    {
        public CriarProfessorRequestValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(6)
                .WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }       
    }
}
