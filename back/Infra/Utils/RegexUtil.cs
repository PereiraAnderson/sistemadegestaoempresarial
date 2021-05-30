using System.Text.RegularExpressions;

namespace SGE.Infra.Utils
{
    public static class RegexUtil
    {
        public static string ApenasNumeros(string texto)
        {
            return Regex.Replace(texto, "[^0-9]", "");
        }
    }
}