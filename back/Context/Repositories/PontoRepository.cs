using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Extensions;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories
{
    public class EnderecoRepository : SGERepository<Ponto>, IPontoRepository
    {

        public EnderecoRepository(SGEDbContext context) : base(context)
        {
        }

        public IOrderedQueryable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
             GetAll().AplicaFiltro(filtro).AplicaOrdenacao(ordenacao);

        public Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            new Paginacao<Ponto>(Get(filtro, ordenacao), paginacao);
    }
}