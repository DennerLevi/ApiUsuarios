using ApiUsuarios.Dto;
using ApiUsuarios.Models;
using Microsoft.Data.SqlClient;

namespace ApiUsuarios.Service
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        public UsuarioService( IConfiguration configuration) { 
        
        _configuration = configuration;
        }

        public Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

            }
        
        }
    }
}
   