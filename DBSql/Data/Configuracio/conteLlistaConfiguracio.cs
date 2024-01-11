using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class conteLlistaConfiguracio : IEntityTypeConfiguration<ConteLlista>
    {
        public void Configure(EntityTypeBuilder<ConteLlista> builder)
        {
            builder.HasKey(x => new { x.NomLlista, x.MAC, x.UID });
            builder.HasOne(x => x.llistaObj).WithMany(x => x.conteLlista).HasForeignKey(x => new { x.NomLlista, x.MAC });
        }
    }
}