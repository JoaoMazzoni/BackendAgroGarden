using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

public class Insumo
{
    [Key]
    public string CodigoLote { get; set; }

    public string NomeDoInsumo { get; set; }

    public string Funcao { get; set; }

    public string FornecedorId { get; set; }

    public Fornecedor Fornecedor { get; set; }

    public int QuantidadeEntrada { get; set; }

    public int MililitrosAtual { get; set; }

    public DateTime DataValidade { get; set; }

    public DateTime DataEntrada { get; set; }

    [AllowedValues("Ativo", "Inativo")]
    public string Status { get; set; }
}