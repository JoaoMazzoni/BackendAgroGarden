using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Endereco
    {
        [Key]
        public string Id { get; set; }

        [JsonProperty("logradouro")]
        public string Rua { get; set; }

        public string Numero { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Cidade { get; set; }

        [JsonProperty("uf")]
        public string Estado { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

    }
}
