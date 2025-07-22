
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Dominio.Entidades;

namespace Usuarios.Infraestructura.Adaptadores.Configuraciones
{
    public class PerfilConfiguracion : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToView("vw_datosusuario");
            builder.HasNoKey();
            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Username).HasColumnName("username");
            builder.Property(c => c.Nombres).HasColumnName("nombres");
            builder.Property(c => c.Apellidos).HasColumnName("apellidos");
            builder.Property(c => c.Correo).HasColumnName("correo");
            builder.Property(c => c.Telefono).HasColumnName("telefono");
            builder.Property(c => c.IdRol).HasColumnName("idrol");
            builder.Property(c => c.Rol).HasColumnName("rol");
            builder.Property(c => c.FechaCreacion).HasColumnName("fecharegistro");
            builder.Property(c => c.FechaModificacion).HasColumnName("fechaactualizacion");
            builder.Property(c => c.IdPerfil).HasColumnName("idperfil");
        }
    }
}
