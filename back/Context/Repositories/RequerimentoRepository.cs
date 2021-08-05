using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Extensions;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories
{
    public class RequerimentoRepository : SGERepository<Requerimento>, IRequerimentoRepository
    {

        public RequerimentoRepository(SGEDbContext context) : base(context)
        {
        }

        public IOrderedQueryable<Requerimento> Get(RequerimentoFiltro filtro = null, Ordenacao ordenacao = null) =>
             GetAll().AplicaFiltro(filtro).AplicaOrdenacao(ordenacao);

        public Paginacao<Requerimento> Get(Paginacao paginacao, RequerimentoFiltro filtro = null, Ordenacao ordenacao = null) =>
            new Paginacao<Requerimento>(Get(filtro, ordenacao), paginacao);
    }
}