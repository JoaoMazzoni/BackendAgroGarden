using System.ComponentModel.DataAnnotations;

public class ProdutoVenda
{
    [Key]
    public int Id { get; set; }
    public Produto Produto { get; set; }
    public long Quantidade { get; set; }
    public int VendaId { get; set; }
}