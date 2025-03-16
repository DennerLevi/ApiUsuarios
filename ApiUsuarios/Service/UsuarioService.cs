using ApiUsuarios.Dto;
using ApiUsuarios.Models;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiUsuarios.Service
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {

            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");

                if (usuarioBanco.Count() == 0)
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
        public async Task<ResponseModel<UsuarioDto>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<UsuarioDto> response = new ResponseModel<UsuarioDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where id = @id", new { Id = usuarioId });

                if (usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum Usuário localizado";
                    response.Status = false;
                    return response;
                }

                //minha lista de usuario vai ser uma lista usuarioDTO
                var usuarioMapeado = _mapper.Map<UsuarioDto>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuario localizados com sucesso";
            }
            return response;
        }

        public async Task<ResponseModel<List<UsuarioDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("CriarUsuario", usuarioCriarDto, commandType: CommandType.StoredProcedure);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro";
                    response.Status = false;
                    return response;
                }
                var usuarios = await ListarUsuarios(connection);

                // Mapeia a lista de entidades 'Usuario' para uma lista de objetos 'UsuarioDto'.

                var usuarioMapeados = _mapper.Map<List<UsuarioDto>>(usuarios);

                response.Dados = usuarioMapeados;
                response.Mensagem = "Usuarios listados com sucesso";

                return response;

            }
        }

        public async Task<ResponseModel<List<UsuarioEditarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioEditarDto>> response = new ResponseModel<List<UsuarioEditarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("AtualizandoUsuario", usuarioEditarDto, commandType: CommandType.StoredProcedure);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao atualizar";
                    response.Status = false;
                    return response;

                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioEditarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso";

                return response;
            }
        }
        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioDto>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from Usuarios where id = @Id", new {Id = usuarioId });

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao Remover";
                    response.Status = false;
                    return response;
                }
                var usuarios = await ListarUsuarios(connection);

                var usuarioMapeados = _mapper.Map<List<UsuarioDto>>(usuarios);

                response.Dados = usuarioMapeados;
                response.Mensagem = "Usuário removido com sucesso!";
                return response;

            }
        }
    }
}
