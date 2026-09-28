namespace AdaptaEdu.Application.DTOs
{
    public record CriarAlunoRequest(
      string Nome,
      DateOnly DataNascimento,
      List<RespostaQuestionarioDto> Respostas
  );

    public record RespostaQuestionarioDto(string Pergunta, bool Resposta);
}
