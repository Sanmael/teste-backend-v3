using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Infrastructure.Data.Configurations;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnType("uuid");

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.PlayType)
               .HasConversion<string>()
               .IsRequired()
               .HasMaxLength(50);

        builder.OwnsOne(x => x.Lines, linesBuilder =>
        {
            linesBuilder.Property(x => x.Value)
                .HasColumnName("Lines")
                .IsRequired();
        });
    }
}