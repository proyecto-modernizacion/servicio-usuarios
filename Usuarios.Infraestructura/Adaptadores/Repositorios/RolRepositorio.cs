
using Usuarios.Dominio.Entidades;
using Usuarios.Dominio.ObjetoValor;
using Usuarios.Dominio.Puertos.Repositorios;
using Usuarios.Infraestructura.Adaptadores.RepositorioGenerico;

namespace Usuarios.Infraestructura.Adaptadores.Repositorios
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly IRepositorioBase<Rol> _repositorioRol;

        public RolRepositorio(IRepositorioBase<Rol> repositorioRol)
        {
            _repositorioRol = repositorioRol;
        }
        public async Task<Rol> ObtenerPorId(Guid id)
        {
            return await _repositorioRol.BuscarPorLlave(id);
        }
    }
}
