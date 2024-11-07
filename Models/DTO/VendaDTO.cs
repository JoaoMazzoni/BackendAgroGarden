
using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class VendaDTO
    {  
        public string DocumentoCliente { get; set; }
        
        public string DocumentoFuncionario { get; set; }
        
        public List<ProdutoVendaDTO> ProdutoVenda { get; set; }

        [AllowedValues("Dinheiro", "Cartão de Crédito", "Cartão de Débito", "Pix")]
        public string FormaPagamento { get; set; }
    }
}
