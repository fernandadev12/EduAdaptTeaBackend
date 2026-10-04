namespace AdaptaEdu.Domain.Entities;

// Domain/Entities/Aluno.cs
public class Aluno
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public Guid ResponsavelId { get; set; }
    public ICollection<PerfilAdaptacao> Perfis { get; set; } = new List<PerfilAdaptacao>();
    public List<RespostaQuestionario> Respostas { get; set; } = new();
    public Guid ProfessorId { get; set; }
}