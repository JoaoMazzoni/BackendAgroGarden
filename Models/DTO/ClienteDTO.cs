
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class ClienteDTO
    {
        [Key]
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}
