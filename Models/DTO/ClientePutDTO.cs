
using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class ClientePutDTO
    {
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoDTO Endereco { get; set; }

        [AllowedValues("Ativo", "Inativo")]
        public string Status { get; set; }
    }
}
