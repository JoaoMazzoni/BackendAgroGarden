using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Colheita
    {
        [Key]
        public int NumeroRegistro { get; set; }
        
        public Plantacao Plantacao { get; set; }
        
        public DateTime DataColheita { get; set; }
        
        public int QuantidadeColhida { get; set; }

        [AllowedValues("Concluída", "Cancelada", "Expirada", "Consumida")]
        public string Status { get; set; }
    }
}
