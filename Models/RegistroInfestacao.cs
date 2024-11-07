using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RegistroInfestacao
    {
        [Key]
        public int Id { get; set; }
        
        public Plantacao Plantacao { get; set; }
        
        public Praga Praga { get; set; }
        
        public DateTime DataRegistro { get; set; }
        
        public DateTime? DataConclusaoTratamento { get; set; }

        [AllowedValues("Sim", "Não", "Em processo")]
        public string Cauterizado { get; set; }

        [AllowedValues("Ativo", "Perda", "Concluido")]
        public string Status { get; set; }
    }
}