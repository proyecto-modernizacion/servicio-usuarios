
using AutoMapper;
using MediatR;
using System.Net;
using Usuarios.Aplicacion.Comun;
using Usuarios.Aplicacion.Usuario.Dto;
using Usuarios.Aplicacion.Usuario.Herramientas;
using Usuarios.Dominio.Servicios.Usuarios;

namespace Usuarios.Aplicacion.Usuario.Comandos
{
    public class UsuarioCrearManejador : IRequestHandler<UsuarioCrearComando, UsuarioCreadoOut>
    {
        private readonly IMapper _mapper;
        private readonly CrearUsuario _crearUsuario;

        public UsuarioCrearManejador(IMapper mapper, CrearUsuario crearUsuario)
        {
            _mapper = mapper;
            _crearUsuario = crearUsuario;
        }
        public async Task<UsuarioCreadoOut> Handle(UsuarioCrearComando request, CancellationToken cancellationToken)
        {
            UsuarioCreadoOut output = new ();

            try 
            { 
                var usuarioNuevo = _mapper.Map<Dominio.Entidades.Usuario>(request);
                usuarioNuevo.Contrasena = Utilidades.Cifrar(usuarioNuevo.Contrasena);
                var resultado = await _crearUsuario.Ejecutar(usuarioNuevo);
                output.IdUsuario = resultado.Id;
                output.IdRol = resultado.IdRol;
                output.Mensaje = "Usuario creado correctamente";
                output.Resultado = Resultado.Exitoso;
                output.Status = HttpStatusCode.Created;
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
