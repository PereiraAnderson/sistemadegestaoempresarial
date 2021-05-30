using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Context.Models
{
    public class Ponto : GenericModel
    {
        [Required]
        public DateTimeOffset Data { get; set; }

        [Required]
        [ForeignKey("UsuarioId")]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
