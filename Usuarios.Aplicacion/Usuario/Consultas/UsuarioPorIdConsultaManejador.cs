
using AutoMapper;
using MediatR;
using System.Net;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;
using Usuarios.Dominio.Servicios.Usuarios;

namespace Usuarios.Aplicacion.Usuario.Consultas
{

    public class UsuarioPorIdConsultaManejador : IRequestHandler<UsuarioPorIdConsulta, UsuarioOut>
    {
        private readonly IMapper _mapper;
        private readonly ConsultarUsuario _consultarUsuario;
        public UsuarioPorIdConsultaManejador(IMapper mapper, ConsultarUsuario consultarUsuario)
        {
            _mapper = mapper;
            _consultarUsuario = consultarUsuario;
        }
        public async Task<UsuarioOut> Handle(UsuarioPorIdConsulta request, CancellationToken cancellationToken)
        {
            UsuarioOut output = new();

            try
            {
                var usuario = await _consultarUsuario.EjecutarUsuarioPorPerfil(request.Id);

                if (usuario is null)
                {
                    output.Mensaje = "Usuario no encontrado";
                    output.Resultado = Resultado.SinRegistros;
                    output.Status = HttpStatusCode.NotFound;
                }
                else 
                {
                    output = _mapper.Map<UsuarioOut>(usuario);
                    output.Mensaje = "Consulta exitosa";
                    output.Resultado = Resultado.Exitoso;
                    output.Status = HttpStatusCode.OK;
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
    }
}
