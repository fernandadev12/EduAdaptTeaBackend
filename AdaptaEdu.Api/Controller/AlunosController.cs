// AdaptaEdu.Api/Controllers/AlunosController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Professor")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService) => _alunoService = alunoService;

    [HttpPost]
    public async Task<ActionResult<Guid>> Criar([FromBody] CriarAlunoRequest request)
    {
        var professorId = User.GetUserId();
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
        var professorId = User.GetUserId();
        var alunos = await _alunoService.ListarPorProfessorAsync(professorId);
        return Ok(alunos);
    }
}