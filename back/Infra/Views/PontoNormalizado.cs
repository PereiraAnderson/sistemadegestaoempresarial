using System;
using System.Diagnostics.CodeAnalysis;

namespace SGE.Infra.Views
{
    [ExcludeFromCodeCoverage]
    public class PontoNormalizado
    {
        public DateTimeOffset Entrada { get; set; }

        public DateTimeOffset Saida { get; set; }

        public DateTimeOffset Jornada { get; set; }

        public string JornadaString { get; set; }

        public string Tarefas { get; set; }
    }
}