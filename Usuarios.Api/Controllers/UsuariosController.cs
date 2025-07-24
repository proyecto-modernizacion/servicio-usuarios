
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Api.Helpers;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Comandos;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Api.Controllers
{
    /// <summary>
    /// Controlador de atributos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// constructor
        /// </summary>
        public UsuariosController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Iniciar sesión en el sistema
        /// </summary>
        /// <response code="200"> 
        /// LoginOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginOut), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginComando login)
        {
            var output = await _mediator.Send(login);

            if (output.Resultado == Resultado.Exitoso)
            {
                return Ok(output);
            }
            else if (output.Resultado == Resultado.SinRegistros) 
            { 
                return NotFound();
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Autoriza el acceso
        /// </summary>
        /// <response code="200"> 
        /// TokenInfo: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("Autorizar")]
        [ProducesResponseType(typeof(TokenInfo), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public IActionResult Autorizar()
        {
            var output = new TokenInfo
            {
                Username = HttpContext.Items["UserId"].ToString(),
                Mensaje = "Operación exitosa",
                Resultado = Resultado.Exitoso,
                Status = System.Net.HttpStatusCode.OK
            };
            
            return Ok(output);
            
        }

    }
}
