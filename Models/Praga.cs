using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

public class Praga
{
    [Key]
    public string PragaId { get; set; }

    [AllowedValues("Pulgões","Lagartas", "Cochonilhas", "Lesmas", "Caracóis", "Moscas", "Mosquitos", "Trips", "Ácaros", "Besouros", "Formigas", "Nematóides", "Cigarras", "Brocas", "Percevejos", "Grilos", "Gafanhotos", "Minhocas Cortadeiras", "Bicho Carpinteiro", "Vermes", "Psilídeos", "Cicadelídeos", "Bicho da Luz", "Aranhas", "Gorgulhos", "Oídio", "Ferrugem", "Míldio", "Antracnose", "Fusarinose", "Mofo", "Cytospora", "Mancha Foliar", "Podridão")]
    public string NomeDaPraga { get; set; }

    [AllowedValues("Inseto", "Fungo", "Bactéria", "Aracnídeo", "Verme", "Molusco", "Larva")]
    public string TipoPraga { get; set; }

    [AllowedValues("Químico, Biológico, Cultural")]
    public string TipoControle {  get; set; }
   
    public string ControleAdequado { get; set; }
}