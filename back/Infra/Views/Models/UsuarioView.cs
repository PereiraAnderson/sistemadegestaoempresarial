using System;
using SGE.Infra.Enums;

namespace SGE.Infra.Views.Models
{
    public class UsuarioView
    {
        public string Id { get; set; }

        public bool Ativo { get; set; }

        public DateTimeOffset DataCriacao { get; set; }

        public DateTimeOffset? DataModificacao { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Senha { get; set; }

        public EnumUsuarioPerfil Perfil { get; set; }
    }
}
