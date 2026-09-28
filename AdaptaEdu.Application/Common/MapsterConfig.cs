using System;
using System.Collections.Generic;
using System.Text;
using AdaptaEdu.Application.Alunos.DTOs;
using AdaptaEdu.Application.DTOs;
using AdaptaEdu.Domain.Entities;
using Mapster;

namespace AdaptaEdu.Application.Common
{
    // Application/Common/MapsterConfig.cs  (sem mudanças)
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Aluno, AlunoDto>.NewConfig()
                .Map(dest => dest.Respostas, src => src.Perfis
                    .SelectMany(p => p.Respostas)
                    .Select(r => new RespostaQuestionarioDto(r.Pergunta, r.Resposta)));
        }
    }
}
