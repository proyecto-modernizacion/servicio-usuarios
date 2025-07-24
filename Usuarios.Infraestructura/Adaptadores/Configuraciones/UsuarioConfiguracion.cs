
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using Usuarios.Dominio.Entidades;

namespace Usuarios.Infraestructura.Adaptadores.Configuraciones
{
    [ExcludeFromCodeCoverage]
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasNoKey();

            builder.Property(u => u.Clave).HasColumnName("clave");
            builder.Property(u => u.Usr_codigo).HasColumnName("usr_codigo");
        }
    }
}
