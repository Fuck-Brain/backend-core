using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Application.Database.EntityConfiguration
{
    public class TaxDbConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder
                .Property<Guid>("OrderId");  // Defining a shadow property
        }
    }
}