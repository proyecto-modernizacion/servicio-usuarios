
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;
using Usuarios.Dominio.Servicios.Usuarios;

namespace Usuarios.Aplicacion.Usuario.Comandos
{
    public class UsuarioLoginManejador : IRequestHandler<UsuarioLoginComando, LoginOut>
    {
        private readonly ConsultarUsuario _consultarUsuario;
        private readonly string _llave;
        private readonly string _receptor;
        private readonly string _emisor;
        private readonly int _tiempoWeb;

        public UsuarioLoginManejador(IConfiguration configuracion, ConsultarUsuario consultarUsuario) 
        {
            _consultarUsuario = consultarUsuario;
            _llave = configuracion["TokenJwt:Llave"];
            _receptor = configuracion["TokenJwt:Receptor"];
            _emisor = configuracion["TokenJwt:Emisor"];
            _tiempoWeb = int.Parse(configuracion["TokenJwt:TiempoWeb"]);
        }
        public async Task<LoginOut> Handle(UsuarioLoginComando request, CancellationToken cancellationToken)
        {
            LoginOut output = new();

            try
            {
                var usuario = await _consultarUsuario.EjecutarPorUsername(request.Username);

                if (usuario == null)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "Usuario no encontrado";
                    output.Status = HttpStatusCode.NotFound;
                }
                else 
                { 

                    if (request.Contrasena == usuario.Clave)
                    {
                        output.Token = GenerarTokenJwt(usuario.Usr_codigo, _tiempoWeb);
                        output.Username = usuario.Usr_codigo;
                        output.Mensaje = "Operación exitosa";
                        output.Resultado = Resultado.Exitoso;
                        output.Status = HttpStatusCode.OK;
                    }
                    else 
                    {
                        output.Mensaje = "Usuario y/o clave incorrecta";
                        output.Resultado = Resultado.Error;
                        output.Status = HttpStatusCode.Unauthorized;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-" + ex.InnerException.Message);
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }

        private string GenerarTokenJwt(string codigoUsuario, int tiempo)
        {
            DateTime dtNow = DateTime.UtcNow;
            SymmetricSecurityKey securityKey = new (System.Text.Encoding.Default.GetBytes(_llave.PadRight((512 / 8), '\0')));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.Name, codigoUsuario.ToString())]);
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(audience: _receptor, issuer: _emisor, subject: claimsIdentity, notBefore: dtNow, expires: dtNow.AddMinutes(tiempo), signingCredentials: signingCredentials);

            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
