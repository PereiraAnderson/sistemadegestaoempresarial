using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SGE.Infra.Views
{
    [ExcludeFromCodeCoverage]
    public class RelatorioPonto
    {
        public string Funcionario { get; set; }

        public List<PontoNormalizado> Pontos { get; set; }

        public DateTimeOffset Saldo { get; set; }

        public string SaldoString { get; set; }
    }
}