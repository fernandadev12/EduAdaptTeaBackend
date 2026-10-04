using System.Security.Claims;
using AdaptaEdu.Application.Common;
using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Application.Services.Interfaces;
using AdaptaEdu.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AdaptaEdu.Application.Services
{
   public class ProfessorService : IProfessorService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IValidator<CriarProfessorRequest> _validator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public ProfessorService(
            UserManager<Usuario> userManager,
            IValidator<CriarProfessorRequest> validator,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _validator = validator;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Guid> CriarAsync(CriarProfessorRequest request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var existente = await _userManager.FindByEmailAsync(request.Email);
            if (existente is not null)
                throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                UserName = request.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, request.Senha);
            if (!resultado.Succeeded)
                throw new InvalidOperationException(string.Join("; ", resultado.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(usuario, "Professor");

            return usuario.Id;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario is null) return null;

            var senhaValida = await _userManager.CheckPasswordAsync(usuario, request.Senha);
            if (!senhaValida) return null;

            var roles = await _userManager.GetRolesAsync(usuario);
            var token = _jwtTokenGenerator.Gerar(usuario, roles);

            return new LoginResponse(token, usuario.Id, usuario.Nome);
        }

        public Task<Guid> ObterProfessorId(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
