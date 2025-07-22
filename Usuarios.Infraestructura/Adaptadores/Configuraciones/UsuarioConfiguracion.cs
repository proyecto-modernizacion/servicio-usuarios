

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
            builder.ToTable("tbl_usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .IsRequired();

            builder.Property(x => x.Contrasena)
                .HasColumnName("contrasena")
                .IsRequired();

            builder.Property(x => x.Nombres)
                .HasColumnName("nombres")
                .IsRequired();

            builder.Property(x => x.Apellidos)
                .HasColumnName("apellidos")
                .IsRequired();

            builder.Property(x => x.Correo)
                .HasColumnName("correo")
                .IsRequired();

            builder.Property(x => x.Telefono)
                .HasColumnName("telefono")
                .IsRequired();

            builder.Property(x => x.IdRol)
                .HasColumnName("idrol")
                .IsRequired();

            builder.Property(x => x.FechaCreacion)
                .HasColumnName("fecharegistro")
                .HasColumnType("timestamp(6)")
                .IsRequired();

            builder.Property(x => x.FechaModificacion)
                .HasColumnName("fechaactualizacion")
                .HasColumnType("timestamp(6)")
                .IsRequired(false);

        }
    }
}
