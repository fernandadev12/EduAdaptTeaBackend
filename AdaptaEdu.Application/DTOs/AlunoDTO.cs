using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptaEdu.Application.DTOs
{
    public class AlunoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public List<RespostaQuestionarioDto> Respostas { get; set; } = new();
    }

}
