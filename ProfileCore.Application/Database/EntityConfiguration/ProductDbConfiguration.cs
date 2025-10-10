using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileCore.Domain.Entity;

namespace ProfileCore.Application.Database.EntityConfiguration
{
    public class ProductDbConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property<Guid>("OrderId");  // Defining a shadow property
        }
    }
}