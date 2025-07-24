
using Usuarios.Dominio.Entidades;

namespace Usuarios.Dominio.Puertos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObtenerPorUsername(string username);
        
    }
}
