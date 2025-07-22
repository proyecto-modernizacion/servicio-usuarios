
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;
using Usuarios.Aplicacion.Usuario.Herramientas;
using Usuarios.Dominio.Servicios.Usuarios;

namespace Usuarios.Aplicacion.Usuario.Comandos
{
    public class UsuarioLoginManejador : IRequestHandler<UsuarioLoginComando, LoginOut>
    {
        private readonly ConsultarUsuario _consultarUsuario;
        private readonly ConsultarRolUsuario _consultarRol;
        private readonly string _llave;
        private readonly string _receptor;
        private readonly string _emisor;
        private readonly int _tiempoWeb;
        private readonly int _tiempoMovil;

        public UsuarioLoginManejador(IConfiguration configuracion, ConsultarUsuario consultarUsuario, ConsultarRolUsuario consultarRol) 
        {
            _consultarUsuario = consultarUsuario;
            _consultarRol = consultarRol;
            _llave = configuracion["TokenJwt:Llave"];
            _receptor = configuracion["TokenJwt:Receptor"];
            _emisor = configuracion["TokenJwt:Emisor"];
            _tiempoWeb = int.Parse(configuracion["TokenJwt:TiempoWeb"]);
            _tiempoMovil = int.Parse(configuracion["TokenJwt:TiempoMovil"]);

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
                    var claveCifrada = Utilidades.Cifrar(request.Contrasena);

                    if (claveCifrada == usuario.Contrasena)
                    {
                        var rol = await _consultarRol.Ejecutar(usuario.IdRol);
                        output.Menu = rol.Menu;
                        output.Idusuario = usuario.Id;

                        int tiempo = 0;
                        if (request.Aplicacion == AplicacionEnumerador.MOVIL)
                        {
                            tiempo = _tiempoMovil;
                        }
                        else 
                        {
                            tiempo = _tiempoWeb;
                        }
                        output.Token = GenerarTokenJwt(usuario.Id, tiempo);
                        output.Mensaje = "Operación exitosa";
                        output.Resultado = Resultado.Exitoso;
                        output.Status = HttpStatusCode.OK;
                    }
                    else 
                    {
                        output.Mensaje = "Usuario y/o clave incorrecta";
                        output.Resultado = Resultado.Error;
                        output.Status = HttpStatusCode.InternalServerError;
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

        private string GenerarTokenJwt(Guid idUsuario, int tiempo)
        {
            DateTime dtNow = DateTime.UtcNow;
            SymmetricSecurityKey securityKey = new (System.Text.Encoding.Default.GetBytes(_llave.PadRight((512 / 8), '\0')));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity([new Claim(ClaimTypes.Name, idUsuario.ToString())]);
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(audience: _receptor, issuer: _emisor, subject: claimsIdentity, notBefore: dtNow, expires: dtNow.AddMinutes(tiempo), signingCredentials: signingCredentials);

            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
