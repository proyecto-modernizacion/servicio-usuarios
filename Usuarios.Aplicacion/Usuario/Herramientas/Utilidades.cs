
using System.Security.Cryptography;

namespace Usuarios.Aplicacion.Usuario.Herramientas
{
    public static class Utilidades
    {
        public static string Cifrar(string texto)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(texto);
            using var sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
