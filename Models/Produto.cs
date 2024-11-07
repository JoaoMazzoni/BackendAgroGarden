using McMaster.Extensions.CommandLineUtils;
using Models;
using System.ComponentModel.DataAnnotations;

public class Produto
{
    [Key]
    public string Lote { get; set; } /*Lote = (Codigo da Plantacao da Colheita => Colheita.Plantacao)*/
    
    public string NomeProduto { get; set; } //Nome da plantacao

    [AllowedValues("Frutas", "Legumes", "Verduras")]
    public string CategoriaProduto { get; set; }
    
    public Colheita ColheitaOrigem { get; set; }
    
    public long Quantidade { get; set; } //Quantidade colhida na colheita
    
    public decimal ValorUnitario { get; set; }
   
    public DateTime DataValidade { get; set; } //10 dias após a colheita

    [AllowedValues("Ativo", "Inativo", "Vencido")]
    public string Status { get; set; }
}