using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Infrastructure.Data.Configurations;

namespace TheatricalPlayersRefactoring.Infrastructure.Persistence;

public class TheatricalContext : DbContext
{
    public TheatricalContext(DbContextOptions<TheatricalContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Play> Plays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceConfiguration());
        modelBuilder.ApplyConfiguration(new PlayConfiguration());
    }
}
