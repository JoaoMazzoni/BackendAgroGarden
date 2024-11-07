using System.Text.RegularExpressions;

namespace FazendaAPI.Utils
{
    public class ValidarTelefone
    {
       
        public bool TelefoneValidar(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return false;
            }

            var regex = new Regex(@"^\([1-9]{2}\) [2-9][0-9]{3,4}\-[0-9]{4}$");
            return regex.IsMatch(telefone);
        }

        public string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
            {
                return string.Empty;
            }

            return Regex.Replace(telefone, @"(\d{2})(\d{4,5})(\d{4})", "($1) $2-$3");
        }
    }
}