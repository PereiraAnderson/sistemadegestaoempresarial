using System.Linq;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories.Interfaces
{
    public interface IPontoRepository : ISGERepository<Ponto>
    {
        IOrderedQueryable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null);
    }
}