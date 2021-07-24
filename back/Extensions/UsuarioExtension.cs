using System.Linq;
using Microsoft.EntityFrameworkCore;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Views.Models;

namespace SGE.Extensions
{
    public static class UsuarioExtension
    {
        public static Usuario ToModel(this UsuarioView view)
        {
            var model = new Usuario
            {
                Nome = view.Nome,
                CPF = view.CPF,
                Perfil = view.Perfil,
                Login = view.Login,
                Senha = view.Senha
            };

            GenericViewExtension.ToModel(view, model);
            return model;
        }

        public static UsuarioView ToView(this Usuario model)
        {
            var view = new UsuarioView
            {
                Nome = model.Nome,
                CPF = model.CPF,
                Perfil = model.Perfil,
                Login = model.Login,
                Senha = model.Senha
            };

            GenericViewExtension.ToView(model, view);
            return view;
        }

        public static IQueryable<Usuario> AplicaFiltro(this IQueryable<Usuario> query, UsuarioFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrWhiteSpace(filtro.CPF))
                    query = query.Where(x => x.CPF.Contains(filtro.CPF));

                if (!string.IsNullOrWhiteSpace(filtro.Login))
                    query = query.Where(x => x.Login.Contains(filtro.Login));

                if (!string.IsNullOrWhiteSpace(filtro.Nome))
                    query = query.Where(x => x.Nome.Contains(filtro.Nome));

                query = query.AplicaGenericFilter(filtro);
            }

            return query;
        }
    }
}