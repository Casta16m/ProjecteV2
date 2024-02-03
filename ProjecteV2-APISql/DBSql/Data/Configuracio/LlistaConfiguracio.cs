using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class LlistaConfiguracio : IEntityTypeConfiguration<Llista>
    {
        /// <summary>
        /// Configuracio de la llista
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Llista> builder)
        {
            builder.HasKey(x => new { x.Nom, x.ID_MAC});
        }
    }
}