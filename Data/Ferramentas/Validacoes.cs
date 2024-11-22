using System.Text.RegularExpressions;

namespace AgendaContatoApi.Data.Ferramentas
{
    public static partial class Geral
    {
        public static bool EhSomenteNumero(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return true;

            return valor.All(char.IsDigit);
        }
        public static bool ehEmailValido(this string email)
        {
            var rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (rg.IsMatch(email))
                return true;

            return false;
        }
        public static bool ehTelefoneCelular(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return false;

            var novoValor = valor.RemoveCaracteresDoTelefone();

            if (long.TryParse(novoValor, out long novo))
                return true;

            return false;
        }
        public static bool ehInstagram(this string url)
        {
            //padroes de URL do Instagram
            string[] instagram = { "instagram.com", "instagram.com/", "@" };

            // Verifica se a URL contem algum dos padroes do Instagram
            return instagram.Any(padrao => url.Contains(padrao));
        }
        public static bool ehFacebook(this string url)
        {
            //padroes de URL do facebook
            string[] facebook = { "facebook.com", "facebook.com/" };

            return facebook.Any(padrao => url.Contains(padrao));
        }
        public static bool ehLinkedIn(this string url)
        {
            //padroes de URL do LinkedIn
            string[] linkedIn = { "linkedin.com", "linkedin.com/in/", "linkedin.com/company/" };

            return linkedIn.Any(padrao => url.Contains(padrao));
        }
        public static bool ehCpfValido(string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;
            return cpf.EndsWith(digito);
        }
        public static bool ehCnpjValido(string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "");
            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;
            return cnpj.EndsWith(digito);
        }
    }
}