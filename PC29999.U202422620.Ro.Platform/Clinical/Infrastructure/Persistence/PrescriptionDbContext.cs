using Microsoft.EntityFrameworkCore;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;

namespace PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence;

public sealed class PrescriptionDbContext(DbContextOptions<PrescriptionDbContext> options) : DbContext(options)
{
    public DbSet<Prescription> Prescriptions => Set<Prescription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionDbContext).Assembly);
    }
}
