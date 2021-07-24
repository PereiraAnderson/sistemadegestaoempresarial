using System.ComponentModel.DataAnnotations;
using SGE.Infra.Enums;

namespace SGE.Context.Models
{
    public class Usuario : GenericModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public EnumUsuarioPerfil Perfil { get; set; }
    }
}
