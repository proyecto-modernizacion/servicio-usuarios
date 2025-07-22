
using System.Diagnostics.CodeAnalysis;
using Usuarios.Dominio.ObjetoValor;

namespace Usuarios.Dominio.Puertos.Repositorios
{
    
    public interface IRolRepositorio
    {
        Task<Rol> ObtenerPorId(Guid id);
    }
}
