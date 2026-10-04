// AdaptaEdu.Api/Controllers/AlunosController.cs
using System.Security.Claims;
using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Professor")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoService _alunoService;
    private readonly IProfessorService _professorService;

    public AlunosController(IAlunoService alunoService, IProfessorService professorService)
    {
        _alunoService = alunoService;
        _professorService = professorService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CriarAluno([FromBody] CriarAlunoRequest request)
    {
        Guid professorId = await _professorService.ObterProfessorId(User);
        var alunoId = await _alunoService.CriarAsync(request, professorId);
        return CreatedAtAction(nameof(ObterPorId), new { id = alunoId }, alunoId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> ObterPorId(Guid id)
    {
        var aluno = await _alunoService.ObterPorIdAsync(id);
        return aluno is null ? NotFound() : Ok(aluno);
    }

    [HttpGet]
    public async Task<ActionResult<List<AlunoDto>>> ListarMeusAlunos()
    {
        Guid professorId = await _professorService.ObterProfessorId(User);
        var alunos = await _alunoService.ListarPorProfessorAsync(professorId);
        return Ok(alunos);
    }

}