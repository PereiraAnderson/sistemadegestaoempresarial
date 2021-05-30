using System.Collections.Generic;
using System.Threading.Tasks;
using SGE.Context.Models;
using SGE.Infra.Enums;
using SGE.Infra.Filters;
using SGE.Infra.Utils;
using SGE.Infra.Views;
using SGE.Infra.Views.Models;

namespace SGE.Services.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null);
        Usuario Get(string id, IEnumerable<string> includes = null);
        Usuario GetByCPF(string cpf, IEnumerable<string> includes = null);
        Usuario GetByEmail(string email, IEnumerable<string> includes = null);
        Usuario Add(Usuario usuario, EnumUsuarioPerfil perfil);
        Usuario Update(UsuarioView usuarioView);
        Usuario Remove(Usuario usuario);
        Usuario Remove(string id);
        Task<LoginOut> Login(LoginIn login);
        Usuario AlterarSenha(AlterarSenha alteraSenha);
    }
}