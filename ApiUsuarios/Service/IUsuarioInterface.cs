using ApiUsuarios.Dto;
using ApiUsuarios.Models;

namespace ApiUsuarios.Service
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios();

        Task<ResponseModel<UsuarioDto>> BuscarUsuarioPorId(int usuarioId);

        Task<ResponseModel<List<UsuarioDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);

        Task<ResponseModel<List<UsuarioEditarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);

        Task<ResponseModel<List<UsuarioDto>>> RemoverUsuario(int id);
    }
}
