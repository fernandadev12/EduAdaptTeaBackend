using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services;
using AdaptaEdu.Application.Validation;
using AdaptaEdu.Domain.Entities;
using AdaptaEdu.Domain.Repositories.Interfaces;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace AdaptaEdu.Tests.Alunos
{
    // AdaptaEdu.Tests/Alunos/AlunoServiceTests.cs
    public class AlunoServiceTests
    {
        [Fact]
        public async Task Deve_Criar_Aluno_Com_Perfil_E_Respostas()
        {
            // Arrange
            var repositoryMock = new Mock<IAlunoRepository>();
            var validator = new CriarAlunoRequestValidator();
            var service = new AlunoService(repositoryMock.Object, validator);

            var request = new CriarAlunoRequest(
                Nome: "João",
                DataNascimento: new DateOnly(2015, 3, 10),
                Respostas: new List<RespostaQuestionarioDto>
                {
                new("Tem dificuldade com textos longos?", true),
                new("Precisa de apoio visual/imagens?", true)
                });

            // Act
            var alunoId = await service.CriarAsync(request, Guid.NewGuid());
            
            // Assert
            alunoId.Should().NotBeEmpty();
            repositoryMock.Verify(r => r.AdicionarAsync(
                It.Is<Aluno>(a => a.Nome == "João" && a.Perfis.First().Respostas.Count() == 2)),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Sem_Respostas_Do_Questionario()
        {
            var repositoryMock = new Mock<IAlunoRepository>();
            var validator = new CriarAlunoRequestValidator();
            var service = new AlunoService(repositoryMock.Object, validator);

            var request = new CriarAlunoRequest("João", new DateOnly(2015, 3, 10), new());

            var act = async () => await service.CriarAsync(request, Guid.NewGuid());

            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
