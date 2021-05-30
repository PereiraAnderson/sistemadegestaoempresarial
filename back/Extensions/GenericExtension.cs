using SGE.Context.Models;
using SGE.Infra.Views.Models;

public static class GenericViewExtension
{
    public static void ToView(GenericModel model, GenericView view)
    {
        view.Id = model.Id;
        view.Ativo = model.Ativo;
        view.DataCriacao = model.DataCriacao;
        view.DataModificacao = model.DataModificacao;
    }

    public static void ToModel(GenericView view, GenericModel model)
    {
        model.Id = view.Id;
        model.Ativo = view.Ativo;
        model.DataCriacao = view.DataCriacao;
        model.DataModificacao = view.DataModificacao;
    }
}