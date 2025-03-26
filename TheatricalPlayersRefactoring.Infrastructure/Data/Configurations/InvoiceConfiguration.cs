using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Infrastructure.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnType("uuid");

        builder.OwnsOne(x => x.Customer, customerBuilder =>
        {
            customerBuilder.Property(x => x.Value)
                .HasColumnName("CustomerName")
                .IsRequired();
        });

        builder.OwnsOne(x => x.TotalCredits, creditsBuilder =>
        {
            creditsBuilder.Property(x => x.Value)
                .HasColumnName("Credits")
                .IsRequired();
        });

        builder.HasMany(x => (ICollection<Performance>)x.Performances)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}