using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Context.Models;

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
                    Id = "77ef37fd-1868-4293-9993-b113de673962",
                    Ativo = true,
                    DataCriacao = DateTimeOffset.ParseExact("01/01/2020 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("pt-BR")),
                    Nome = "Usuário Administrador SGE",
                    UserName = "admin@sge.com",
                    NormalizedUserName = "admin@sge.com".Normalize().ToUpperInvariant(),
                    Email = "admin@sge.com",
                    NormalizedEmail = "admin@sge.com".Normalize().ToUpperInvariant(),
                    SecurityStamp = "NVF3LXGDRJ4XKL3TDAPE4J2GKR2EN5GV",
                    ConcurrencyStamp = "a65bb329-5c2a-4228-86e9-dea7a90f13b2",
                    PasswordHash = "AQAAAAEAACcQAAAAEEpH3YlMIfrNTmvVe/caWaS0oUP2Fz98PkSauo3hTotGPyfVXaS4/Cdg8a/r02LaYg=="
                }
            );
        }
    }
}