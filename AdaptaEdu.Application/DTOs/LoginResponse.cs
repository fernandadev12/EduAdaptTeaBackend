namespace AdaptaEdu.Application.DTOs
{
    public record LoginResponse(string Token, Guid ProfessorId, string Nome);
}
