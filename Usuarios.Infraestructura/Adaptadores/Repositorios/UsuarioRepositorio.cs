using Usuarios.Dominio.Entidades;
using Usuarios.Dominio.Puertos.Repositorios;
using Usuarios.Infraestructura.Adaptadores.RepositorioGenerico;
using System.Diagnostics.CodeAnalysis;

namespace Usuarios.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IRepositorioBase<Usuario> _repositorioUsuario;
        private readonly IRepositorioBase<Perfil> _repositorioPerfil;

        public UsuarioRepositorio(IRepositorioBase<Usuario> repositorioUsuario, IRepositorioBase<Perfil> repositorioPerfil)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPerfil = repositorioPerfil;
        }

        public Task<Usuario> Actualizar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            return await _repositorioUsuario.Guardar(usuario);
        }

        public Task<Usuario> Eliminar(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ObtenerPorId(Guid id)
        {
            return await _repositorioUsuario.BuscarPorLlave(id);
        }

        public async Task<Usuario> ObtenerPorUsername(string username)
        {
            return await _repositorioUsuario.BuscarPorCampos(x => x.Username == username);
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _repositorioUsuario.DarListado();
        }

        public async Task<Perfil> ObtenerUsuarioPorPerfil(Guid idUsuario)
        {
            return await _repositorioPerfil.BuscarPorCampos(x => x.Id == idUsuario);
        }
    }
}
