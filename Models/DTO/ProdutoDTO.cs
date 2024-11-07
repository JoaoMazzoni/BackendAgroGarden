using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class ProdutoDTO
    {
        [AllowedValues("Frutas", "Legumes", "Verduras")]
        public string CategoriaProduto { get; set; }
        public int ColheitaOrigem { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
