
using McMaster.Extensions.CommandLineUtils;

namespace Models.DTO
{
    public class VendaPutStatusDTO
    {        
        public string DocumentoFuncionario { get; set; }

        [AllowedValues("Concluida", "Cancelada")]
        public string Status { get; set;}
    }
}
