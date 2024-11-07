using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        
        public Cliente Cliente { get; set; }
        
        public Usuario Funcionario { get; set; }
        
        public List<ProdutoVenda> Produtos { get; set; }

        public string DataDaVenda { get; set; }

        [AllowedValues ("Dinheiro", "Cartão de Crédito", "Cartão de Débito", "Pix")]
        public string FormaPagamento { get; set; }
        
        public decimal ValorTotal { get; set; }

        [AllowedValues("Concluida", "Cancelada")]
        public string Status { get; set; }

        public bool Ativa { get; set; }

        public Venda()
        {
        }

        public Venda(Cliente cliente, Usuario funcionario, List<ProdutoVenda> produtos, string formaPagamento)
        {
            Cliente = cliente;
            Funcionario = funcionario;
            Produtos = produtos;
            ValorTotal = CalcularValorTotal();
            DataDaVenda = DataDaVendaAtual();
            FormaPagamento = formaPagamento;
            Status = "Concluida";
            Ativa = true;
        }

        public decimal CalcularValorTotal()
        {
            ValorTotal = 0;
            foreach (var produto in Produtos)
                ValorTotal += produto.Produto.ValorUnitario * Convert.ToDecimal(produto.Quantidade) ;
            return ValorTotal;
        }
        public string DataDaVendaAtual()
        {
            DataDaVenda = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return DataDaVenda;
        }   
    }
}