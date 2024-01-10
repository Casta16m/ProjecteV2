using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class EsConfiguration : IEntityTypeConfiguration<Es>
    {
        public void Configure(EntityTypeBuilder<Es> builder)
        {
            builder.HasKey(x => new { x.UID, x.Nom });
            builder.HasOne(x => x.CançoObj).WithMany(x => x.Es).HasForeignKey(x => x.UID);
            builder.HasOne(x => x.FormatObj).WithMany(x => x.Es).HasForeignKey(x => x.Nom);
            builder.HasIndex(x => x.UID).IsUnique(false);
            builder.HasIndex(x => x.Nom).IsUnique(false);
        }
    }
}