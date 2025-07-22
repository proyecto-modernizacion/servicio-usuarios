
using Usuarios.Dominio.Entidades;
using Usuarios.Dominio.Puertos.Repositorios;

namespace Usuarios.Dominio.Servicios.Usuarios
{
    public class CrearUsuario (IUsuarioRepositorio usuarioRepositorio)
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;

        public async Task<Usuario> Ejecutar(Usuario usuario)
        {
            await ValidarUsuario(usuario);
            usuario.Id = Guid.NewGuid();
            usuario.FechaCreacion = DateTime.Now;
            return await _usuarioRepositorio.Crear(usuario);

        }

        private async Task<bool> ValidarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepositorio.ObtenerPorUsername(usuario.Username);

            if (usuarioExistente != null)
            {
                throw new ArgumentException("El nombre de usuario ya existe.");
            }

            return true;
        }


    }
}
