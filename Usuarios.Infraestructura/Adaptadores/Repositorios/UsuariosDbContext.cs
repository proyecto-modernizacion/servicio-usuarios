using Microsoft.EntityFrameworkCore;
using Usuarios.Dominio.Entidades;
using System.Diagnostics.CodeAnalysis;
using Usuarios.Infraestructura.Adaptadores.Configuraciones;


namespace Usuarios.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options): base(options){ }

        public DbSet<Usuario> Usuarios { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        }
    }
}
