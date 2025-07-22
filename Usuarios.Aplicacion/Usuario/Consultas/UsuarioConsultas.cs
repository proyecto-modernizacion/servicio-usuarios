
using MediatR;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Aplicacion.Usuario.Consultas
{
    public record UsuarioPorIdConsulta (
        Guid Id
        ) : IRequest<UsuarioOut>;
}
