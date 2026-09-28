using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptaEdu.Application.Common
{
    public interface IJwtTokenGenerator
    {
        string Gerar(Usuario usuario, IList<string> roles);
    }
}
