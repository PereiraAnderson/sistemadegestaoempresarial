using System.ComponentModel.DataAnnotations;

namespace SGE.Infra.Views
{
    public class LoginIn
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}