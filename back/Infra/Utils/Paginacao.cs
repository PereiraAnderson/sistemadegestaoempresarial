using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.Infra.Utils
{
    public class Paginacao<T> where T : class
    {
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IEnumerable<T> ListaItens { get; set; }

        public Paginacao(IOrderedQueryable<T> origem, Paginacao parametros)
        {
            int total = origem.Count();
            int totalPaginas = parametros.ListaTodos ? 1 : (int)Math.Ceiling(total / (double)parametros.Tamanho);

            TotalItens = total;
            TotalPaginas = totalPaginas;
            TamanhoPagina = parametros.ListaTodos ? total : parametros.Tamanho;
            NumeroPagina = parametros.Pagina;
            ListaItens = parametros.ListaTodos ? origem : origem.Skip(parametros.Skip()).Take(parametros.Tamanho);
        }

        public Paginacao()
        {

        }
    }

    public class Paginacao
    {
        public bool ListaTodos { get; set; } = false;
        public int Pagina { get; set; } = 1;
        public int Tamanho { get; set; } = 10;
        public int Skip() => Tamanho * (Pagina - 1);
    }
}
