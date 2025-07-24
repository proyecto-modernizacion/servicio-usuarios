
using Usuarios.Dominio.Entidades;
using Usuarios.Dominio.Puertos.Repositorios;

namespace Usuarios.Dominio.Servicios.Usuarios
{
    public class ConsultarUsuario(IUsuarioRepositorio usuarioRepositorio)
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
        public async Task<Usuario> EjecutarPorUsername(string username)
        {
            return await _usuarioRepositorio.ObtenerPorUsername(username);
        }
    }
}
