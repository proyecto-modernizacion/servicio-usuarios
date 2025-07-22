
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Api.Helpers;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Comandos;
using Usuarios.Aplicacion.Usuario.Consultas;
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
        /// Crea un usuario nuevo en el sistema
        /// </summary>
        /// <response code="201"> 
        /// UsuarioOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpPost]
        [Route("Crear")]
        [ProducesResponseType(typeof(UsuarioCreadoOut), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Crear([FromBody] UsuarioCrearComando usuario)
        {
            var output = await _mediator.Send(usuario);

            if (output.Resultado != Resultado.Error)
            {
                return Created(string.Empty, output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
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
        [ProducesResponseType(typeof(LoginOut), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginComando login)
        {
            var output = await _mediator.Send(login);

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Autoriza el acceso a una aplicación
        /// </summary>
        /// <param name="aplicacion">Identificador de la aplicación: 1 - web, 2 - appmovil, 3 - Api interna</param>
        /// <response code="200"> 
        /// TokenInfo: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("Autorizar/{aplicacion}")]
        [ProducesResponseType(typeof(TokenInfo), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public IActionResult Autorizar([FromRoute] int aplicacion)
        {
            var output = new TokenInfo
            {
                IdUsuario = HttpContext.Items["UserId"].ToString(),
                Mensaje = "Operación exitosa",
                Resultado = Resultado.Exitoso,
                Status = System.Net.HttpStatusCode.OK
            };
            
            return Ok(output);
            
        }

        /// <summary>
        /// Obtiene la información de un usuario por su ID
        /// </summary>
        /// <response code="200"> 
        /// UsuarioOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("{idusuario}")]
        [ProducesResponseType(typeof(UsuarioOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseOut), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> ConsultarPorId([FromRoute] Guid idusuario)
        {
            var output = await _mediator.Send(new UsuarioPorIdConsulta(idusuario));

            if (output.Resultado == Resultado.SinRegistros)
            {
                return NotFound(new { output.Resultado, output.Mensaje, output.Status });
            }
            else if (output.Resultado == Resultado.Exitoso)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

    }
}
