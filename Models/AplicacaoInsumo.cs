

using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Models
{
    public class AplicacaoInsumo
    {
        [Key]
        public int Registro { get; set; }
        public Plantacao Plantacao { get; set; }
        public Insumo Insumo { get; set; }

        [AllowedValues("Agrotoxico, Fertilizante, Adubo")]
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataAplicacao { get; set; }

        [AllowedValues("Aplicado", "Cancelado")]
        public string Status { get; set; }


    }
}
