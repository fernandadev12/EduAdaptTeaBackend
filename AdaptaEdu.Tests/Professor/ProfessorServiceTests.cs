using AdaptaEdu.Application.Common;
using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
                 Mock.Of<IUserStore<Usuario>>(),
                 Mock.Of<IOptions<IdentityOptions>>(),
                 new PasswordHasher<Usuario>(),
                 new List<IUserValidator<Usuario>>(),
                 new List<IPasswordValidator<Usuario>>(),
                 new UpperInvariantLookupNormalizer(),
                 new IdentityErrorDescriber(),
                 Mock.Of<IServiceProvider>(),
                 Mock.Of<ILogger<UserManager<Usuario>>>());

            userManagerMock
                .Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Usuario { Email = "joao@escola.com" });

            var request = new AdaptaEdu.Application.DTOs.CriarProfessorRequest("João", "joao@escola.com", "senha123");

            var validatorMock = new Mock<IValidator<AdaptaEdu.Application.DTOs.CriarProfessorRequest>>();
            validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult()); 

            var service = new AdaptaEdu.Application.Services.ProfessorService(
                userManagerMock.Object, validatorMock.Object, Mock.Of<IJwtTokenGenerator>());

            var act = async () => await service.CriarAsync(request);

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Login_Deve_Retornar_Null_Se_Usuario_Nao_Existe()
        {
            var userManagerMock = new Mock<UserManager<Usuario>>(
                            Mock.Of<IUserStore<Usuario>>(),
                            Mock.Of<IOptions<IdentityOptions>>(),
                            new PasswordHasher<Usuario>(),
                            new List<IUserValidator<Usuario>>(),
                            new List<IPasswordValidator<Usuario>>(),
                            new UpperInvariantLookupNormalizer(),
                            new IdentityErrorDescriber(),
                            Mock.Of<IServiceProvider>(),
                            Mock.Of<ILogger<UserManager<Usuario>>>());

            userManagerMock
                .Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);


            var request = new AdaptaEdu.Application.DTOs.CriarProfessorRequest("João", "joao@escola.com", "senha123");

            var validatorMock = new Mock<IValidator<AdaptaEdu.Application.DTOs.CriarProfessorRequest>>();
            validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult()); 

            var service = new AdaptaEdu.Application.Services.ProfessorService(
                userManagerMock.Object, validatorMock.Object, Mock.Of<IJwtTokenGenerator>());
           
            var resultado = await service.LoginAsync(new LoginRequest("naoexiste@escola.com", "123456"));

            resultado.Should().BeNull();
        }
    }
}
