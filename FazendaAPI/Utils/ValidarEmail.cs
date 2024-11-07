using System.Text.RegularExpressions;

namespace FazendaAPI.Utils
{
    public class ValidarEmail
    {
        public bool EmailValidar (string email)
        { 
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)\.com$");
            }
        }
    }
}