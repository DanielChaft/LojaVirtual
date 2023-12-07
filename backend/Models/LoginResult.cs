using System;

namespace FichaCadastral.Models
{
    public class LoginResult : BaseResult
    {
        public Guid UsuarioGuid { get; internal set; }
    }
}
