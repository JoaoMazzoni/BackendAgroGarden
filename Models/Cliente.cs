
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cliente
    {
        [Key]
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        [AllowedValues("Ativo", "Inativo")]
        public string Status { get; set; }
    }
}
