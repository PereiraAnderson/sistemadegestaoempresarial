using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Context.Models;
using SGE.Infra.Enums;

namespace SGE.Context.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasData(
                new Usuario
                {
                    Id = 1,
                    Ativo = true,
                    DataCriacao = DateTimeOffset.ParseExact("01/01/2020 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("pt-BR")),
                    Nome = "Usuário Administrador SGE",
                    Login = "admin@sge.com",
                    // 123456
                    Senha = "$2a$11$llgzqoCJoRHxX00w3rqdwO8yN1/xvq1w.UERLsN4KjTFo/2Dvk3mS",
                    CPF = "00000000000",
                    Perfil = EnumUsuarioPerfil.SGE
                }
            );
        }
    }
}