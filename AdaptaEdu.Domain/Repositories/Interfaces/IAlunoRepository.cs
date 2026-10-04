using AdaptaEdu.Domain.Entities;

namespace AdaptaEdu.Domain.Repositories.Interfaces
{
    public interface IAlunoRepository
    {
        Task AdicionarAsync(Aluno aluno);
        Task<IEnumerable<Aluno>> ListarPorProfessorAsync(Guid professorId);
        Task<Aluno> ObterPorIdAsync(Guid id);
    }
}
