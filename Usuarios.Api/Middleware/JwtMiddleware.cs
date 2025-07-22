using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Api.Middleware
{
    /// <summary>
    /// Middleware para validar token Jwt
    /// El token debe llegar en el header en forma: "Authorization: Bearer token"
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// constructor de la clase middleware
        /// </summary>
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// Valida el token emitido con llaves de configuración en appSetings que comparte con SecurityApi.
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (token != null)
            {
                var tokenInfo = ValidarToken(token);
                if (!string.IsNullOrEmpty(tokenInfo.IdUsuario)) 
                {
                    httpContext.Items["UserId"] = tokenInfo.IdUsuario;
                }
            }

            await _next(httpContext);
        }

        private TokenInfo ValidarToken(string token)
        {
            TokenInfo output = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_configuration["TokenJwt:Llave"].PadRight(512 / 8, '\0'));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["TokenJwt:Emisor"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["TokenJwt:Receptor"],
                    ClockSkew = TimeSpan.FromSeconds(5),
                }, out SecurityToken securityToken);

                var jwtToken = (JwtSecurityToken)securityToken;
                output.IdUsuario = jwtToken.Claims.First(t => t.Type == "unique_name").Value;
                
                return output;
            }
            catch
            {
                return output;
            }
        }

    }
}
