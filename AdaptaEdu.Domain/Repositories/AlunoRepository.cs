using AdaptaEdu.Domain.Entities;
using AdaptaEdu.Domain.Repositories.Interfaces;

namespace AdaptaEdu.Domain.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        public Task AdicionarAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aluno>> ListarPorProfessorAsync(Guid professorId)
        {
            throw new NotImplementedException();
        }

        public Task<Aluno> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
