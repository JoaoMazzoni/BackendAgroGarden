

using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class InsumoDTO
    {
        public string NomeDoInsumo { get; set; }

        [AllowedValues("Fertilizante", "Adubo", "Inseticida", "Fungicida", "Herbicida", "Acaricidas", "Nematicidas", "Moluscicidas", "Bactericidas")]
        public string Funcao { get; set; }
        public string FornecedorCNPJ { get; set; }
        public int QuantidadeEntrada { get; set; }
        public string DataValidade { get; set; }
    }
}
