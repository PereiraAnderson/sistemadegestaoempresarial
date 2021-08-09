using System.Linq;
using SGE.Context.Models;
using SGE.Infra.Filters;
using SGE.Infra.Views.Models;

namespace SGE.Extensions
{
    public static class RequerimentoExtension
    {
        public static Requerimento ToModel(this RequerimentoView view)
        {
            var model = new Requerimento
            {
                Justificativa = view.Justificativa,
                Status = view.Status,
                PontoId = view.PontoId,
                UsuarioId = view.UsuarioId
            };

            GenericViewExtension.ToModel(view, model);
            return model;
        }

        public static RequerimentoView ToView(this Requerimento model)
        {
            var view = new RequerimentoView
            {
                Justificativa = model.Justificativa,
                Status = model.Status,
                PontoId = model.PontoId,
                UsuarioId = model.UsuarioId,
                Usuario = model.Usuario?.ToView(),
                Ponto = model.Ponto?.ToView()
            };

            GenericViewExtension.ToView(model, view);
            return view;
        }

        public static IQueryable<T> AplicaFiltro<T>(this IQueryable<T> query, RequerimentoFiltro filtro)
            where T : Requerimento
        {
            if (filtro != null)
            {
                query = query.AplicaGenericFilter(filtro);

                if (filtro.UsuarioId.HasValue)
                    query = query.Where(x => x.UsuarioId == filtro.UsuarioId);
            }

            return query;
        }
    }
}