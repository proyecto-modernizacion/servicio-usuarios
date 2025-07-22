
using MediatR;
using System.ComponentModel.DataAnnotations;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Aplicacion.Usuario.Comandos
{
    public record UsuarioCrearComando(
        [Required(ErrorMessage = "El campo Username es obligatorio")]
        string Username,
        [Required(ErrorMessage = "El campo Contrasena es obligatorio")]
        string Contrasena,
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        string Nombres,
        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        string Apellidos,
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        string Correo,
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        string Telefono,
        [Required(ErrorMessage = "El campo IdRol es obligatorio")]
        Guid? IdRol
        ) : IRequest<UsuarioCreadoOut>;

    public record UsuarioLoginComando(
        [Required(ErrorMessage = "El campo Username es obligatorio")]
        string Username,
        [Required(ErrorMessage = "El campo Contrasena es obligatorio")]
        string Contrasena,
        AplicacionEnumerador Aplicacion
        ) : IRequest<LoginOut>;

}
