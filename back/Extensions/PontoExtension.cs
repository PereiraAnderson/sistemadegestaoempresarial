using System.Linq;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Views.Models;

namespace SGE.Extensions
{
    public static class PontoExtension
    {
        public static Ponto ToModel(this PontoView view)
        {
            var model = new Ponto
            {
                Data = view.Data,
                Tarefa = view.Tarefa,
                UsuarioId = view.UsuarioId
            };

            GenericViewExtension.ToModel(view, model);
            return model;
        }

        public static PontoView ToView(this Ponto model)
        {
            var view = new PontoView
            {
                Data = model.Data,
                Tarefa = model.Tarefa,
                UsuarioId = model.UsuarioId,
                Usuario = model.Usuario?.ToView()
            };

            GenericViewExtension.ToView(model, view);
            return view;
        }

        public static IQueryable<T> AplicaFiltro<T>(this IQueryable<T> query, PontoFiltro filtro)
            where T : Ponto
        {
            if (filtro != null)
            {
                query = query.AplicaGenericFilter(filtro);

                if (filtro.UsuarioId.HasValue)
                    query = query.Where(x => x.UsuarioId == filtro.UsuarioId);

                if (filtro.Hoje.HasValue)
                    query = query.Where(x => x.Data.Date.CompareTo(System.DateTime.Today) == 0);
            }

            return query;
        }
    }
}