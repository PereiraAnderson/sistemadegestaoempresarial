using System.ComponentModel.DataAnnotations;
using SGE.Infra.Enums;

namespace SGE.Infra.Views.Models
{
    public class RequerimentoView : GenericView
    {
        [Required]
        public string Justificativa { get; set; }

        public EnumRequerimentoStatus Status { get; set; }

        [Required]
        public long UsuarioId { get; set; }
        public UsuarioView Usuario { get; set; }

        [Required]
        public long PontoId { get; set; }
        public PontoView Ponto { get; set; }
    }
}
