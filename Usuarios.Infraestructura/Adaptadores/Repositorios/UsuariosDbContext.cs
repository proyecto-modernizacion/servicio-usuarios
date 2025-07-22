using Microsoft.EntityFrameworkCore;
using Usuarios.Dominio.Entidades;
using System.Diagnostics.CodeAnalysis;
using Usuarios.Infraestructura.Adaptadores.Configuraciones;
using Usuarios.Dominio.ObjetoValor;

namespace Usuarios.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options): base(options){ }

        public DbSet<Usuario> Usuarios { get; set;}
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new RolConfiguracion());
            modelBuilder.ApplyConfiguration(new PerfilConfiguracion());
        }
    }
}
