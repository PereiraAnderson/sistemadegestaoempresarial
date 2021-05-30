using System;
using System.ComponentModel.DataAnnotations;

namespace SGE.Context.Models
{
    public class GenericModel
    {
        [Key]
        public long Id { get; set; }

        public bool Ativo { get; set; }

        public DateTimeOffset DataCriacao { get; set; }

        public DateTimeOffset? DataModificacao { get; set; }

    }
}
