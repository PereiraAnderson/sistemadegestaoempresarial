using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class RequerimentoService : IRequerimentoService
    {
        private readonly IRequerimentoRepository _repo;

        public RequerimentoService(IRequerimentoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Requerimento> Get(RequerimentoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Requerimento> Get(Paginacao paginacao, RequerimentoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Requerimento Get(long id, IEnumerable<string> includes = null) =>
            _repo.Get(id, includes);

        public Requerimento Add(Requerimento requerimento)
        {
            var ret = _repo.Add(requerimento);
            _repo.SaveChanges();
            return ret;
        }

        public void Add(IEnumerable<Requerimento> requerimentos)
        {
            _repo.Add(requerimentos);
            _repo.SaveChanges();
        }

        public Requerimento Update(Requerimento requerimento)
        {
            var ret = _repo.Update(requerimento);
            _repo.SaveChanges();
            return ret;
        }

        public void Update(IEnumerable<Requerimento> requerimentos)
        {
            _repo.Update(requerimentos);
            _repo.SaveChanges();
        }

        public Requerimento Remove(Requerimento requerimento)
        {
            var ret = _repo.Remove(requerimento);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<Requerimento> requerimentos)
        {
            _repo.Remove(requerimentos);
            _repo.SaveChanges();
        }

        public Requerimento Remove(long id)
        {
            var ret = _repo.Remove(id);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<dynamic> ids)
        {
            _repo.Remove(ids);
            _repo.SaveChanges();
        }
    }
}