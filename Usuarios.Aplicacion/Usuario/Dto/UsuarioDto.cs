
using Usuarios.Aplicacion.Comun;

namespace Usuarios.Aplicacion.Usuario.Dto
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Guid IdRol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class UsuarioCreadoOut : BaseOut
    {
        public Guid IdUsuario { get; set; }
        public Guid IdRol { get; set; }
    }

    public class UsuarioOut : BaseOut
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Guid IdRol { get; set; }
        public string Rol { get; set; }
        public Guid IdPerfil { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }

    public class LoginOut : BaseOut 
    { 
        public string Token { get; set; }
        public string Menu { get; set; }
        public Guid Idusuario { get; set; }

    }
}
