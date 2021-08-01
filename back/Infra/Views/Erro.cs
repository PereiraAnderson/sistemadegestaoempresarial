using System.Diagnostics.CodeAnalysis;

namespace SGE.Infra.Views
{
    [ExcludeFromCodeCoverage]
    public class Erro
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        public string Detalhes { get; set; }

    }
}