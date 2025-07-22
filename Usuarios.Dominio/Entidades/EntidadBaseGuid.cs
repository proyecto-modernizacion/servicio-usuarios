
using System.Diagnostics.CodeAnalysis;

namespace Usuarios.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public abstract class EntidadBaseGuid
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public Guid IdRol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
