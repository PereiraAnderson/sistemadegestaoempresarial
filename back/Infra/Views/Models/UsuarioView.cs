using System;
using SGE.Infra.Enums;

namespace SGE.Infra.Views.Models
{
    public class UsuarioView : GenericView
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public EnumUsuarioPerfil Perfil { get; set; }
    }
}
