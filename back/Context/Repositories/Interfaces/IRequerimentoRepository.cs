using System.Linq;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories.Interfaces
{
    public interface IRequerimentoRepository : ISGERepository<Requerimento>
    {
        IOrderedQueryable<Requerimento> Get(RequerimentoFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Requerimento> Get(Paginacao paginacao, RequerimentoFiltro filtro = null, Ordenacao ordenacao = null);
    }
}