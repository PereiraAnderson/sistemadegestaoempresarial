using System.Collections.Generic;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;

namespace SGE.Services.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Usuario Get(long id, IEnumerable<string> includes = null);
        Usuario GetByCPF(string cpf, IEnumerable<string> includes = null);
        Usuario GetByLogin(string login, IEnumerable<string> includes = null);
        Usuario Add(Usuario usuario);
        Usuario Update(Usuario usuario);
        Usuario Remove(Usuario usuario);
        Usuario Remove(long id);
        LoginOut Login(LoginIn login);
        Usuario AlterarSenha(AlterarSenha alteraSenha);
    }
}