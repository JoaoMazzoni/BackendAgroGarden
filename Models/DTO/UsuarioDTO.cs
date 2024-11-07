using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class UsuarioDTO
    {
        public string CPF { get; set; } 
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        
        [AllowedValues("Administrador", "Agricultor", "Comercial", "Gerente")]
        public string Tipo { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public string Senha { get; set; }
    }
}