

using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AplicacaoInsumoDTO
    {
        public string PlantacaoId { get; set; }
        public string LoteInsumo { get; set; }

        [AllowedValues("Agrotóxico", "Fertilizante", "Adubo")]
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public string DataAplicacao { get; set; }

    }
}
