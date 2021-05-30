using System;

namespace SGE.Infra.Views.Models
{
    public abstract class GenericView
    {
        public long Id { get; set; }

        public bool Ativo { get; set; }

        public DateTimeOffset DataCriacao { get; set; }

        public DateTimeOffset? DataModificacao { get; set; }
    }
}