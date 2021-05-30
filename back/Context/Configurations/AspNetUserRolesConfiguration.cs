using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGE.Context.Configurations
{
    public class AspNetUserClaimsConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "77ef37fd-1868-4293-9993-b113de673962",
                    ClaimType = ClaimTypes.Role,
                    ClaimValue = "SGE"
                }
            );
        }
    }
}