
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Usuarios.Aplicacion.Comun
{
    [ExcludeFromCodeCoverage]
    public class BaseOut
    {
        public Resultado Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
