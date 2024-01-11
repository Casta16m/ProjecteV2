using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class LlistaConfiguracio : IEntityTypeConfiguration<Llista>
    {
        public void Configure(EntityTypeBuilder<Llista> builder)
        {
            builder.HasKey(x => new { x.Nom, x.ID_MAC});
        }
    }
}