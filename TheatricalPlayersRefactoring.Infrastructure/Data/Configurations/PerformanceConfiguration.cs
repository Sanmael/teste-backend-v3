using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Infrastructure.Data.Configurations;

public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnType("uuid");

        builder.HasOne(p => p.Play)
            .WithMany()
            .HasForeignKey(p => p.PlayId)
            .IsRequired();

        builder.HasOne(p => p.Invoice)
            .WithMany(i => i.Performances)
            .IsRequired();

        builder.OwnsOne(p => p.Audience, audience =>
        {
            audience.Property(a => a.Value)
                .HasColumnName("AudienceSize")
                .IsRequired();
        });
    }
}