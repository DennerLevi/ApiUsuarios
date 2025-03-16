using ApiUsuarios.Dto;
using ApiUsuarios.Models;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiUsuarios.Service
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService( IConfiguration configuration, IMapper mapper) { 
        
        _configuration = configuration;
        _mapper = mapper;   
        }

        public async Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco =  await connection.QueryAsync<Usuario>("select * from Usuarios");

                if (usuarioBanco.Count()== 0)
                {
                    response.Mensagem = "Nenhum Usuário localizado";
                    response.Status = false;
                    return response;
                }

                //minha lista de usuario vai ser uma lista usuarioDTO
                var usuarioMapeado = _mapper.Map<List<UsuarioDto>>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuario localizados com sucesso";
            }
            return response;
        }
    }
}
   