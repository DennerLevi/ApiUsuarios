﻿namespace ApiUsuarios.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public bool Situacao { get; set; } //1 - ativo 0 - inativo
    }
}

