
using AutoMapper;
using Usuarios.Aplicacion.Usuario.Dto;

namespace Usuarios.Aplicacion.Usuario.Mapeadores
{
    public class UsuarioMapeador : Profile
    {
        public UsuarioMapeador() 
        {

            CreateMap<Dominio.Entidades.Usuario, UsuarioDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usr_codigo))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Clave));

            CreateMap<Dominio.Entidades.Usuario, UsuarioOut>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usr_codigo))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Clave));


        }
    }
}
