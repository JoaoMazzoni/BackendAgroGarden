
using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class PragaDTO
    {
        [AllowedValues("Pulgoes", "Lagartas", "Cochonilhas", "Lesmas", "Caracois", "Moscas", "Mosquitos", "Trips", "Acaros", "Besouros", "Formigas", "Nematoides", "Cigarras", "Brocas", "Percevejos", "Grilos", "Gafanhotos", "Minhocas Cortadeiras", "Bicho Carpinteiro", "Besouros", "Vermes", "Psilideos", "Cicadelideos", "Bicho da Luz", "Aranhas")]
        public string NomeDaPraga { get; set; }

        [AllowedValues("Inseto", "Fungo", "Bactéria", "Aracnídeo", "Verme", "Molusco", "Larva")]
        public string Tipo { get; set; }

    }
}
