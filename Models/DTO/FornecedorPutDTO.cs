
using McMaster.Extensions.CommandLineUtils;


namespace Models.DTO
{
    public class FornecedorPutDTO
    {
        public string NomeDoFornecedor { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        
        [AllowedValues("Fertilizante", "Agrotóxico", "Adubo", "Geral")]
        public string TipoDeFornecimento { get; set; }
        public EnderecoDTO Endereco { get; set; }

        [AllowedValues("Ativo", "Inativo")]
        public string Status { get; set; }

    }
}
