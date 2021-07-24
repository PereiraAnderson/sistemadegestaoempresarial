using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories.Interfaces
{
    public interface IUsuarioRepository : ISGERepository<Usuario>
    {
        IOrderedQueryable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Usuario GetByCPF(string cpf, IEnumerable<string> includes = null);
        Usuario GetByLogin(string login, IEnumerable<string> includes = null);
    }
}