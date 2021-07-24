using SGE.Infra.Enums;

namespace SGE.Infra.Views
{
    public class LoginOut
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public EnumUsuarioPerfil Perfil { get; set; }
    }
}