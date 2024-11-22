namespace AgendaContatoApi.Data.Ferramentas
{
    public static partial class Geral
    {
        public static bool ExisteOArquivoNaPasta(this string valor)
        {
            var arquivo = new FileInfo(valor);
            return arquivo.Exists;
        }
        public static string ConverteBase64(this string valor) {
            byte[] baseLida = File.ReadAllBytes(valor);
            if (baseLida.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(baseLida);
        }
        public static string ExtraiNomeDoArquivo(this string valor)
        {
            if(string.IsNullOrEmpty(valor)) return string.Empty;

            var arquivo = new FileInfo(valor);
            return arquivo.Name;
        }
        public static string GerarNomeDoArquivo()
        {
            var numeroRandomico = new Random(DateTime.Now.Year);
            numeroRandomico.Next();
            return $"DOC_{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}_{DateTime.Now.Day.ToString("00")}_{numeroRandomico.Next()}";
        }
    }
}