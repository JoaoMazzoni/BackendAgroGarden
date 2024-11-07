namespace FazendaAPI.Utils
{
    public class ValidarCNPJ
    {
        public static bool CNPJValido(string CNPJ)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "").Replace(" ", "");

            if (CNPJ.Length != 14)
            {
                return false;
            }
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj;
            string digito;
            int soma;
            int resto;
            CNPJ = CNPJ.Trim();
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            if (CNPJ.Length != 14)
                return false;
            tempCnpj = CNPJ.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CNPJ.EndsWith(digito);
        }

        public static string Desformatar(string CNPJ)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "").Replace(" ", "");
            return CNPJ;
        }

        public static string FormatarCNPJ(string CNPJ)
        {
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "").Replace(" ", "");
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

    }
}
