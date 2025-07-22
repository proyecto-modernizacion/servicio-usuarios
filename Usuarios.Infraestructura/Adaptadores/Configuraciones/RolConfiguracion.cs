

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using Usuarios.Dominio.ObjetoValor;

namespace Usuarios.Infraestructura.Adaptadores.Configuraciones
{
    [ExcludeFromCodeCoverage]
    public class RolConfiguracion : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("tbl_rol");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasColumnName("descripcion")
                .IsRequired();

            builder.Property(x => x.Menu)
                .HasColumnName("menu")
                .IsRequired();
        }
    }
}
