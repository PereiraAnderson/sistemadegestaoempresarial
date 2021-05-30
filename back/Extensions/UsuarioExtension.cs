using System.Linq;
using Microsoft.EntityFrameworkCore;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Views.Models;

namespace SGE.Extensions
{
    public static class UsuarioExtension
    {
        public static Usuario ToModel(this UsuarioView usuario) =>
            new Usuario
            {
                Id = usuario.Id,
                Ativo = usuario.Ativo,
                DataCriacao = usuario.DataCriacao,
                DataModificacao = usuario.DataModificacao,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                UserName = usuario.Email,
                Email = usuario.Email,
                NormalizedEmail = usuario.Email.Normalize(),
                PasswordHash = usuario.Senha
            };

        public static UsuarioView ToView(this Usuario usuario) =>
            new UsuarioView
            {
                Id = usuario.Id,
                Ativo = usuario.Ativo,
                DataCriacao = usuario.DataCriacao,
                DataModificacao = usuario.DataModificacao,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                Email = usuario.UserName
            };

        public static IQueryable<Usuario> AplicaFiltro(this IQueryable<Usuario> query, UsuarioFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrWhiteSpace(filtro.CPF))
                    query = query.Where(x => x.CPF.Contains(filtro.CPF));

                if (!string.IsNullOrWhiteSpace(filtro.UserName))
                    query = query.Where(x => x.UserName.Contains(filtro.UserName));

                if (!string.IsNullOrWhiteSpace(filtro.Nome))
                    query = query.Where(x => x.Nome.Contains(filtro.Nome));

                if (filtro.Includes != null && filtro.Includes.Count() > 0)
                    foreach (string include in filtro.Includes)
                        query = query.Include(include);

                if (filtro.Ativo.HasValue)
                    query = query.Where(q => q.Ativo == filtro.Ativo);
            }

            return query;
        }
    }
}