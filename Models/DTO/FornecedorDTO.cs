
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class FornecedorDTO
    {
        [Key]
        public string CNPJ { get; set; }
        public string NomeDoFornecedor { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        [AllowedValues("Fertilizante", "Agrotóxico", "Adubo", "Geral")]
        public string TipoDeFornecimento { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}
