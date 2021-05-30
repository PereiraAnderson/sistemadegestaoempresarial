using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SGE.Context.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        public bool Ativo { get; set; }

        [Required]
        public DateTimeOffset DataCriacao { get; set; }

        public DateTimeOffset? DataModificacao { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }
    }
}
