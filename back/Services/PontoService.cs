using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Services.Interfaces;

namespace SGE.Services
{
    public class EnderecoService : IPontoService
    {
        private readonly IPontoRepository _repo;

        public EnderecoService(IPontoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(filtro, ordenacao).AsEnumerable();

        public Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null) =>
            _repo.Get(paginacao, filtro, ordenacao);

        public Ponto Get(long id, IEnumerable<string> includes = null) =>
            _repo.Get(id, includes);

        public Ponto Add(Ponto Ponto)
        {
            var ret = _repo.Add(Ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Add(IEnumerable<Ponto> enderecos)
        {
            _repo.Add(enderecos);
            _repo.SaveChanges();
        }

        public Ponto Update(Ponto Ponto)
        {
            var ret = _repo.Update(Ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Update(IEnumerable<Ponto> enderecos)
        {
            _repo.Update(enderecos);
            _repo.SaveChanges();
        }

        public Ponto Remove(Ponto Ponto)
        {
            var ret = _repo.Remove(Ponto);
            _repo.SaveChanges();
            return ret;
        }

        public void Remove(IEnumerable<Ponto> enderecos)
        {
            _repo.Remove(enderecos);
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