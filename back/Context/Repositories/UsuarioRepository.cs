using System.Collections.Generic;
using System.Linq;
using SGE.Context.Models;
using SGE.Context.Repositories.Interfaces;
using SGE.Extensions;
using SGE.Infra.Filters;
using SGE.Infra.Utils;

namespace SGE.Context.Repositories
{
    public class UsuarioRepository : SGERepository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(SGEDbContext context) : base(context)
        {
        }

        public IOrderedQueryable<Usuario> Get(UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            GetAll().AplicaFiltro(filtro).AplicaOrdenacao(ordenacao);

        public Paginacao<Usuario> Get(Paginacao paginacao, UsuarioFiltro filtro = null, Ordenacao ordenacao = null) =>
            new Paginacao<Usuario>(Get(filtro, ordenacao), paginacao);

        public Usuario GetByCPF(string cpf, IEnumerable<string> includes = null)
        {
            var model = _context.Usuario.SingleOrDefault(x => x.CPF.Equals(cpf));
            if (model != null && includes != null && includes.Count() > 0)
                foreach (string include in includes)
                    Include(model, include.Split('.'));

            return model;
        }

        public Usuario GetByEmail(string email, IEnumerable<string> includes = null)
        {
            var model = _context.Usuario.SingleOrDefault(x => x.Email.Equals(email));
            if (model != null && includes != null && includes.Count() > 0)
                foreach (string include in includes)
                    Include(model, include.Split('.'));

            return model;
        }
    }
}