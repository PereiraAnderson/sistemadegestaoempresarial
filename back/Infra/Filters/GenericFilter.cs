using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SGE.Context.Models;

namespace SGE.Infra.Filters
{
    public abstract class GenericFilter
    {
        public bool? Ativo { get; set; } = true;
        public IEnumerable<string> Includes { get; set; }
    }

    public static class GenericFilterExtension
    {

        public static IQueryable<T> AplicaGenericFilter<T>(this IQueryable<T> query, GenericFilter filtro)
                where T : GenericModel
        {
            if (filtro.Includes != null && filtro.Includes.Count() > 0)
                foreach (string s in filtro.Includes)
                    query = query.Include(s);

            if (filtro.Ativo.HasValue)
                query = query.Where(q => q.Ativo == filtro.Ativo);

            return query;
        }
    }
}
