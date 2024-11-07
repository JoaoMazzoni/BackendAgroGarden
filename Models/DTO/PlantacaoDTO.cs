using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO;
public class PlantacaoDTO
{
    public string Nome { get; set; }

    [AllowedValues("Fruta", "Legume", "Verdura")]
    public string Tipo { get; set; }

    [AllowedValues("Canteiro", "Estufa")]
    public string LocalDePlantio { get; set; }
    public string DataDePlantio { get; set; }

    [AllowedValues("Diaria", "Intercalada", "Semanal")]
    public string Irrigacao { get; set; }

    [AllowedValues("Indireta", "Direta")]
    public string LuzSolar { get; set; }

    [AllowedValues("Frio", "Tropical", "Árido", "Indiferente")]
    public string CondicaoClimatica { get; set; }

    [AllowedValues("Rasteira", "Arbusto", "Árborea", "Parreira")]
    public string Crescimento { get; set; }

}
