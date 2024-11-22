using McMaster.Extensions.CommandLineUtils;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
public class Usuario
{
    [Key]
    public string CPF { get; set; } 
   
    public string NomeCompleto { get; set; }
   
    public DateTime DataNascimento { get; set; }
    
    public string Email { get; set; }
   
    public string Telefone { get; set; }

    [AllowedValues("Administrador", "Agricultor", "Comercial", "Gerente")]
    public string Tipo { get; set;}
    
    public string EnderecoId { get; set; }
    public Endereco Endereco { get; set; }
    
    public string Senha { get; set; }

    public bool Ativo { get; set; }
}