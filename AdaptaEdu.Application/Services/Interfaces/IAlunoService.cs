using AdaptaEdu.Application.DTOs;

namespace AdaptaEdu.Application.Services.Interfaces;


public interface IAlunoService
{
    Task<Guid> CriarAsync(CriarAlunoRequest request, Guid professorId);
    Task<AlunoDto?> ObterPorIdAsync(Guid id);
    Task<List<AlunoDto>> ListarPorProfessorAsync(Guid professorId);
}