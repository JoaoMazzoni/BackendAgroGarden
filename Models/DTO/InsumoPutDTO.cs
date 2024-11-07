using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class InsumoPutDTO
    {
        public string NomeDoInsumo { get; set; }

        [AllowedValues("Fertilizante", "Adubo", "Inseticida", "Fungicida", "Herbicida", "Acaricidas", "Nematicidas", "Moluscicidas", "Bactericidas")]
        public string Funcao { get; set; }
        public int MililitrosAtual { get; set; }
        public string DataValidade { get; set; }

        [AllowedValues("Ativo", "Inativo")]
        public string Status { get; set; }
    }
}
