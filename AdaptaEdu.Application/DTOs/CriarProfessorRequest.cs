using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptaEdu.Application.DTOs
{
    public record CriarProfessorRequest(
     string Nome,
     string Email,
     string Senha
 );

}
