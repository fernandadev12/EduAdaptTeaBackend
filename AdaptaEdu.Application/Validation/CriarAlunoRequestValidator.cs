using AdaptaEdu.Application.DTOs;
using FluentValidation;

namespace AdaptaEdu.Application.Validation
{

    public class CriarAlunoRequestValidator : AbstractValidator<CriarAlunoRequest>
    {
        public CriarAlunoRequestValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Respostas).NotEmpty()
                .WithMessage("O questionário de dificuldades deve ser respondido.");
        }
    }
}
