using System.Collections.Generic;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Services.Interfaces
{
    public interface IPontoService
    {
        IEnumerable<Ponto> Get(PontoFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Ponto> Get(Paginacao paginacao, PontoFiltro filtro = null, Ordenacao ordenacao = null);
        Ponto Get(long id, IEnumerable<string> includes = null);
        Ponto Add(Ponto Ponto);
        void Add(IEnumerable<Ponto> enderecos);
        Ponto Update(Ponto Ponto);
        void Update(IEnumerable<Ponto> enderecos);
        Ponto Remove(Ponto Ponto);
        void Remove(IEnumerable<Ponto> enderecos);
        Ponto Remove(long id);
        void Remove(IEnumerable<dynamic> ids);
    }
}