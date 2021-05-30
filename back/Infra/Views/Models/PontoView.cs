using System;
using System.ComponentModel.DataAnnotations;

namespace SGE.Infra.Views.Models
{
    public class PontoView : GenericView
    {
        [Required]
        public DateTimeOffset Data { get; set; }

        [Required]
        public string UsuarioId { get; set; }
        public UsuarioView Usuario { get; set; }
    }
}
