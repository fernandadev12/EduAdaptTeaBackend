using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services.Interfaces;
using AdaptaEdu.Domain.Entities;
using AdaptaEdu.Domain.Repositories.Interfaces;
using FluentValidation;
using Mapster;

namespace AdaptaEdu.Application.Services
{
    // Application/Alunos/AlunoService.cs
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;
        private readonly IValidator<CriarAlunoRequest> _validator;

        public AlunoService(IAlunoRepository repository, IValidator<CriarAlunoRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Guid> CriarAsync(CriarAlunoRequest request, Guid professorId)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var aluno = new Aluno
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                ProfessorId = professorId
            };

            var perfil = new PerfilAdaptacao
            {
                Id = Guid.NewGuid(),
                AlunoId = aluno.Id,
                Respostas = request.Respostas
                    .Select(r => new RespostaQuestionario { Pergunta = r.Pergunta, Resposta = r.Resposta })
                    .ToList()
            };

            aluno.Perfis.Add(perfil);

            await _repository.AdicionarAsync(aluno);
            return aluno.Id;
        }

        public async Task<AlunoDto?> ObterPorIdAsync(Guid id)
        {
            var aluno = await _repository.ObterPorIdAsync(id);
            return aluno?.Adapt<AlunoDto>();
        }

        public async Task<List<AlunoDto>> ListarPorProfessorAsync(Guid professorId)
        {
            List<Aluno> alunos = new List<Aluno>();
            var result = await _repository.ListarPorProfessorAsync(professorId);
            if (result is not null)
            {
                alunos.AddRange(result);
            }
            return alunos.Adapt<List<AlunoDto>>();
        }
    }
}
