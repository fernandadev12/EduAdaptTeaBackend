using AdaptaEdu.Application.DTOs;

namespace AdaptaEdu.Application.Services.Interfaces
{
    public interface IProfessorService
    {
        Task<Guid> CriarAsync(CriarProfessorRequest request);
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}
