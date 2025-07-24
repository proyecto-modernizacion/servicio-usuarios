using Usuarios.Dominio.Entidades;
using Usuarios.Dominio.Puertos.Repositorios;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Usuarios.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IServiceProvider _serviceProvider;

        public UsuarioRepositorio(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private UsuariosDbContext GetContext()
        {
            return _serviceProvider.GetService<UsuariosDbContext>();
        }

        public async Task<Usuario> ObtenerPorUsername(string username)
        {
            var ctx = GetContext();
            var usuario = await ctx.Usuarios.FromSqlRaw("SELECT Usr_codigo, Clave FROM fun_buscarusuario({0})", username).FirstOrDefaultAsync();
            await ctx.DisposeAsync();
            return usuario;
        }

        
    }
}
