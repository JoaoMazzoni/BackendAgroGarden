using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UsuarioPutDTO
    {
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        
        [AllowedValues("Administrador", "Agricultor", "Comercial", "Gerente")]
        public string Tipo { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
