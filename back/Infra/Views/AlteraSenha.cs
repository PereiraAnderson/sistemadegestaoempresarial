using System.Diagnostics.CodeAnalysis;

namespace SGE.Infra.Views
{
    [ExcludeFromCodeCoverage]
    public class AlterarSenha
    {
        public string Login { get; set; }

        public string SenhaAtual { get; set; }

        public string SenhaNova { get; set; }
    }
}