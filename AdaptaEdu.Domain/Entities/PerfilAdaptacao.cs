using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptaEdu.Domain.Entities
{
    public class PerfilAdaptacao
    {
        public IEnumerable<RespostaQuestionario> Respostas { get; set; } = new List<RespostaQuestionario>();
        public Guid AlunoId { get; set; }
        public Guid Id { get; set; }
    }
}
