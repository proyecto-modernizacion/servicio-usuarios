
using MediatR;
using System.ComponentModel.DataAnnotations;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Aplicacion.Usuario.Comandos
{
    public record UsuarioLoginComando(
        [Required(ErrorMessage = "El campo Username es obligatorio")]
        string Username,
        [Required(ErrorMessage = "El campo Contrasena es obligatorio")]
        string Contrasena
        ) : IRequest<LoginOut>;

}
