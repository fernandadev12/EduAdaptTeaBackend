using AdaptaEdu.Application.Common;
using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services;
using AdaptaEdu.Application.Validation;
using AdaptaEdu.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AdaptaEdu.Tests.Professor
{
    public class ProfessorServiceTests
    {
        private static UserManager<Usuario> CriarUserManagerMock(out Mock<IUserStore<Usuario>> storeMock)
        {
            storeMock = new Mock<IUserStore<Usuario>>();
            return new Mock<UserManager<Usuario>>(
                storeMock.Object, null!, null!, null!, null!, null!, null!, null!, null!).Object;
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Se_Email_Ja_Existe()
        {
            var userManagerMock = new Mock<UserManager<Usuario>>(
                Mock.Of<IUserStore<Usuario>>(), null!, null!, null!, null!, null!, null!, null!, null!);

            userManagerMock
                .Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Usuario { Email = "joao@escola.com" });

            var validator = new CriarProfessorRequestValidator();
            var service = new ProfessorService(userManagerMock.Object, validator, Mock.Of<IJwtTokenGenerator>());

            var request = new CriarProfessorRequest("João", "joao@escola.com", "senha123");

            var act = async () => await service.CriarAsync(request);

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Login_Deve_Retornar_Null_Se_Usuario_Nao_Existe()
        {
            var userManagerMock = new Mock<UserManager<Usuario>>(
                Mock.Of<IUserStore<Usuario>>(), null!, null!, null!, null!, null!, null!, null!, null!);

            userManagerMock
                .Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            var validator = new CriarProfessorRequestValidator();
            var service = new ProfessorService(userManagerMock.Object, validator, Mock.Of<IJwtTokenGenerator>());

            var resultado = await service.LoginAsync(new LoginRequest("naoexiste@escola.com", "123456"));

            resultado.Should().BeNull();
        }
    }
}
