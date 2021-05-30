using System.ComponentModel.DataAnnotations;

namespace SGE.Infra.Views
{
    public class LoginIn
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}