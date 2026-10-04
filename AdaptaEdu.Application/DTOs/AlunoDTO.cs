using System;
using System.Collections.Generic;
using System.Text;
using AdaptaEdu.Domain.Entities;

namespace AdaptaEdu.Application.DTOs
{
    public class AlunoDto
    {
        public Guid Id { get; set; }
        public Guid ResponsavelId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public List<RespostaQuestionarioDto> Respostas { get; set; } = new();
        public ICollection<PerfilAdaptacao> Perfis { get; set; } = new List<PerfilAdaptacao>();
    }

}
