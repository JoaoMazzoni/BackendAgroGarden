using McMaster.Extensions.CommandLineUtils;
using Models;
using System.ComponentModel.DataAnnotations;

public class Fornecedor
{
    [Key]
    public string CNPJ { get; set; }
    
    public string NomeDoFornecedor { get; set; }
    
    public string Telefone { get; set; }
    
    public string Email { get; set; }

    [AllowedValues("Fertilizante", "Agrotoxico", "Adubo", "Geral")]
    public string TipoDeFornecimento { get; set; }
    
    public string EnderecoId { get; set; }
    public Endereco Endereco { get; set; }

    [AllowedValues("Ativo", "Inativo")]
    public string Status { get; set; }
}

