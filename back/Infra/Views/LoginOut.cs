using SGE.Infra.Enums;

namespace SGE.Infra.Views
{
    public class LoginOut
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public EnumUsuarioPerfil Perfil { get; set; }
        public string Token { get; set; }
    }
}