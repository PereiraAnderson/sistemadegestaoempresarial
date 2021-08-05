using System.Collections.Generic;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;

namespace SGE.Services.Interfaces
{
    public interface IRequerimentoService
    {
        IEnumerable<Requerimento> Get(RequerimentoFiltro filtro = null, Ordenacao ordenacao = null);

        Paginacao<Requerimento> Get(Paginacao paginacao, RequerimentoFiltro filtro = null, Ordenacao ordenacao = null);

        Requerimento Get(long id, IEnumerable<string> includes = null);

        Requerimento Add(Requerimento Requerimento);

        void Add(IEnumerable<Requerimento> enderecos);

        Requerimento Update(Requerimento Requerimento);

        void Update(IEnumerable<Requerimento> enderecos);

        Requerimento Remove(Requerimento Requerimento);

        void Remove(IEnumerable<Requerimento> enderecos);

        Requerimento Remove(long id);

        void Remove(IEnumerable<dynamic> ids);
    }
}