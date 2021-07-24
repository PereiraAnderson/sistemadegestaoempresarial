using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class PontoService : IPontoService
    {
        private readonly IPontoRepository _repo;

        public PontoService(IPontoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Ponto Get(long id, IEnumerable<string> includes = null) =>
            _repo.Get(id, includes);

        public Ponto Add(Ponto ponto)
        {
            var ret = _repo.Add(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Add(IEnumerable<Ponto> pontos)
        {
            _repo.Add(pontos);
            _repo.SaveChanges();
        }

        public Ponto Update(Ponto ponto)
        {
            var ret = _repo.Update(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Update(IEnumerable<Ponto> pontos)
        {
            _repo.Update(pontos);
            _repo.SaveChanges();
        }

        public Ponto Remove(Ponto ponto)
        {
            var ret = _repo.Remove(ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<Ponto> pontos)
        {
            _repo.Remove(pontos);
            _repo.SaveChanges();
        }

        public Ponto Remove(long id)
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