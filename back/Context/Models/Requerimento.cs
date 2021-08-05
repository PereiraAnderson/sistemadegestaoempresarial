using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGE.Infra.Enums;

namespace SGE.Context.Models
{
    public class Requerimento : GenericModel
    {
        [Required]
        public string Justificativa { get; set; }

        [Required]
        public EnumRequerimentoStatus Status { get; set; }

        [Required]
        [ForeignKey("UsuarioId")]
        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("PontoId")]
        public long PontoId { get; set; }
        public Ponto Ponto { get; set; }
    }
}
