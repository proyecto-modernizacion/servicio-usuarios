
using Usuarios.Dominio.ObjetoValor;
using Usuarios.Dominio.Puertos.Repositorios;

namespace Usuarios.Dominio.Servicios.Usuarios
{
    public class ConsultarRolUsuario(IRolRepositorio rolRepositorio)
    {
        private readonly IRolRepositorio _rolRepositorio = rolRepositorio;
        public async Task<Rol> Ejecutar(Guid idRol)
        {
            return await _rolRepositorio.ObtenerPorId(idRol);
        }

    }
}
