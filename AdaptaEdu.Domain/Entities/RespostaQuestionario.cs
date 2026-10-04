namespace AdaptaEdu.Domain.Entities
{
    public class RespostaQuestionario
    {
        public Guid Id { get; set; }
        public Guid PerfilAdaptacaoId { get; set; }
        public string Pergunta { get; set; } = string.Empty;
        public bool Resposta { get; set; }
    }
}