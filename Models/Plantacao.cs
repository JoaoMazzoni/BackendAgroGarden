using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

public class Plantacao
{
    [Key]
    public string Id { get; set; }
    public string Nome { get; set; }
    
    [AllowedValues("Fruta", "Legume", "Verdura")]
    public string Tipo { get; set; }

    [AllowedValues("Canteiro", "Estufa")]
    public string LocalDePlantio { get; set; }
    public DateTime DataDePlantio { get; set; }

    [AllowedValues("Diaria", "Intercalada", "Semanal")]
    public string Irrigacao { get; set; }

    [AllowedValues("Indireta", "Direta")]
    public string LuzSolar { get; set; }

    [AllowedValues("Frio", "Tropical", "Árido", "Indiferente")]
    public string CondicaoClimatica { get; set; }

    [AllowedValues("Rasteira", "Arbusto", "Árborea", "Parreira")]
    public string Crescimento { get; set; }

    [AllowedValues("Cultivando", "Colhida", "Perda")]
    public string Colheita { get; set; }

    [AllowedValues("Ativo", "Inativo", "Cultivado")]
    public string Status { get; set; }
}
