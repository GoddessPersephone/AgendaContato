using System.Text.RegularExpressions;

namespace AgendaContatoApi.Data.Ferramentas
{
    public static partial class Geral
    {
        public static string RemoveCaracteres(this string valor)
        {
            try
            {
                if (!string.IsNullOrEmpty(valor))
                {
                    valor = Regex.Replace(valor, @"[^\w\s]", "");
                    return valor.ToUpper();
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string RemoveCaracteresELetras(this string valor)
        {
            try
            {
                if (!string.IsNullOrEmpty(valor))
                {
                    valor = Regex.Replace(valor, @"[^\d]", "");
                    return valor.ToUpper();
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string RemoveCaracteresDoTelefone(this string valor)
        {
            try
            {
                if (!string.IsNullOrEmpty(valor))
                    valor = valor.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");

                return valor;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string CaixaAlta(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;

            return valor.Trim().ToUpper();
        }
        public static string? CaixaAltaRetornaNulo(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return null;

            return valor.Trim().ToUpper();
        }
        public static string CaixaAltaSemEspeciais(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            var novoValor = Regex.Replace(valor, @"[^\w\s]", "");

            return novoValor.Trim().ToUpper();
        }
        public static string CaixaBaixa(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;

            return valor.Trim().ToLower();
        }
        public static string TiraEspacos(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;

            return valor.Trim();
        }
        public static string SeForNuloFicaVazio(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            return valor;
        }

        public static string FormatarCPF(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return null;

            cpf = Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");

            return cpf;
        }
        public static string FormatarCNPJ(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return null;

            cnpj = Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");

            return cnpj;
        }
    }
}