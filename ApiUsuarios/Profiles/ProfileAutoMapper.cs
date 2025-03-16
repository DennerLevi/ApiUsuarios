using ApiUsuarios.Dto;
using ApiUsuarios.Models;
using AutoMapper;

namespace ApiUsuarios.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Usuario, UsuarioEditarDto>();

        }
    }
}
