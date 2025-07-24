
using Usuarios.Aplicacion.Comun;

namespace Usuarios.Aplicacion.Usuario.Dto
{
    public class UsuarioDto
    {
        public string Username { get; set; }
        public string Contrasena { get; set; }

    }

    public class UsuarioOut : BaseOut
    {
        public string Username { get; set; }
        public string Contrasena { get; set; }

    }

    public class LoginOut : BaseOut 
    { 
        public string Token { get; set; }
        public string Username { get; set; }

    }
}
