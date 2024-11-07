namespace FazendaAPI.Utils
{
    public class ValidarCPF
    {
        public static bool Validar(string cpf)
        {
            cpf = Desformatar(cpf);

            if (cpf.Length != 11)
                return false;

            string tempCpf = cpf.Substring(0, 9);

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * (10 - i);
            }

            int checarPrimeiroDigito = 11 - (soma % 11);
            if (checarPrimeiroDigito > 9)
                checarPrimeiroDigito = 0;

            tempCpf += checarPrimeiroDigito;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * (11 - i);
            }

            int checarSegundoDigito = 11 - (soma % 11);

            if (checarSegundoDigito > 9)
                checarSegundoDigito = 0;

            return cpf.EndsWith(checarPrimeiroDigito.ToString() + checarSegundoDigito.ToString());
        }

        public static string Formatar(string cpf)
        {
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }

        public static string Desformatar(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }
    }
}
