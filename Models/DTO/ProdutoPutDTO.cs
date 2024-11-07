using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class ProdutoPutDTO
    {
        [AllowedValues("Frutas", "Legumes", "Verduras")]
        public string CategoriaProduto { get; set; }
        public decimal ValorUnitario { get; set; }

        public int Quantidade { get; set; }

        [AllowedValues("Ativo", "Inativo")]
        public string Status { get; set; }
    }
}
