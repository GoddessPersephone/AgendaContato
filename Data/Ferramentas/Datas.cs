using System.Globalization;

namespace AgendaContatoApi.Data.Ferramentas
{
    public static partial class Geral
    {
        public static DateOnly SomenteData(this DateTime? valor)
        {
            if (valor == null)
                return DateOnly.MinValue;

            return DateOnly.FromDateTime((DateTime)valor);
        }
        public static bool EhNulo(this DateTime? valor)
        {
            if (valor == null) return false; return true;
        }
        public static DateTime? ConverteEmData(this string? valor)
        {
            if (string.IsNullOrEmpty(valor))
                return null;

            DateTime.TryParse(valor, new CultureInfo("pt-BR"), DateTimeStyles.None, out var novaData);
            if (!novaData.Equals(DateTime.MinValue))
                return novaData;

            return null;
        }
        public static DateTime? DataVaziaAssumeNulo(this DateTime? valor)
        {
            return valor.Equals(DateTime.MinValue) ? null : valor;
        }
    }
}