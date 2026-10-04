using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = AdaptaEdu.Application.DTOs.LoginRequest;

namespace AdaptaEdu.Api.Controller
{
    // AdaptaEdu.Api/Controllers/AuthController.cs
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public AuthController(IProfessorService professorService) => _professorService = professorService;

        [HttpPost("professores")]
        public async Task<ActionResult<Guid>> CadastrarProfessor([FromBody] CriarProfessorRequest request)
        {
            var professorId = await _professorService.CriarAsync(request);
            return CreatedAtAction(nameof(CadastrarProfessor), new { id = professorId }, professorId);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var resultado = await _professorService.LoginAsync(request);
            return resultado is null ? Unauthorized("Credenciais inválidas") : Ok(resultado);
        }
    }
}
