using ApiUsuarios.Dto;
using ApiUsuarios.Models;

namespace ApiUsuarios.Service
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios();

        Task<ResponseModel<UsuarioDto>> BuscarUsuarioPorId(int usuarioId);
    }
}
