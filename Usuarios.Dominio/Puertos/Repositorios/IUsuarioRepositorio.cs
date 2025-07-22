
using Usuarios.Dominio.Entidades;

namespace Usuarios.Dominio.Puertos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObtenerPorId(Guid id);
        Task<Usuario> ObtenerPorUsername(string username);
        Task<List<Usuario>> ObtenerTodos();
        Task<Usuario> Crear(Usuario usuario);
        Task<Usuario> Actualizar(Usuario usuario);
        Task<Usuario> Eliminar(Guid id);
        Task<Perfil> ObtenerUsuarioPorPerfil(Guid idUsuario);

    }
}
