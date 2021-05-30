using System.Linq;
using System.Linq.Dynamic.Core;

namespace SGE.Infra.Utils
{
    public static class OrdenacaoExtension
    {
        public static IOrderedQueryable<T> AplicaOrdenacao<T>(this IQueryable<T> query, Ordenacao ordem) =>
            query.OrderBy(ordem == null ? "Id ASC" : ordem.OrdenaPor + (ordem.OrdenacaoAsc ? " ASC" : " DESC"));
    }

    public class Ordenacao
    {
        public string OrdenaPor { get; set; } = "Id";
        public bool OrdenacaoAsc { get; set; } = true;
    }
}
