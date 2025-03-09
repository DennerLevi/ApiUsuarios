namespace ApiUsuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }   
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Salario { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; } //1 - ativo 0 - inativo
        public string Senha { get; set; } 
    }
}
