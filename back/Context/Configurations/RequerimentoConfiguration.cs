using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGE.Context.Models;

namespace SGE.Context.Configurations
{
    public class RequerimentoConfiguration : IEntityTypeConfiguration<Requerimento>
    {
        public void Configure(EntityTypeBuilder<Requerimento> builder)
        {
            builder.HasOne(x => x.Ponto)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}