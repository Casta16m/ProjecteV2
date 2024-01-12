using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class FormatConfiguracio : IEntityTypeConfiguration<Format>
    {
        public void Configure(EntityTypeBuilder<Format> builder)
        {
            builder.HasKey(x => new { x.UID, x.NomFormat });
            builder.HasOne(x => x.CançoObj).WithMany(x => x.Format).HasForeignKey(x => x.UID);
            builder.HasOne(x => x.ExtensioObj).WithMany(x => x.Format).HasForeignKey(x => x.NomFormat);
            builder.HasIndex(x => x.UID).IsUnique(false);
            builder.HasIndex(x => x.NomFormat).IsUnique(false);
        }
    }
}