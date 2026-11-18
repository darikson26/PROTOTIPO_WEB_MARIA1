using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;
using PC29999.U202422620.Ro.Platform.Shared.Domain.ValueObjects;

namespace PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence.Configurations;

public sealed class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("prescriptions");
        builder.HasKey(prescription => prescription.Id);
        builder.Property(prescription => prescription.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(prescription => prescription.PrescriptionNumber)
            .HasConversion(value => value.Value, value => new RoIdentifier(value))
            .HasColumnName("prescription_number").HasMaxLength(9).IsRequired();
        builder.HasIndex(prescription => prescription.PrescriptionNumber).IsUnique();
        builder.Property(prescription => prescription.PatientId)
            .HasConversion(value => value.Value, value => new PartyId(value))
            .HasColumnName("patient_id").HasColumnType("char(36)").IsRequired();
        builder.OwnsOne(prescription => prescription.TreatmentPeriod, duration =>
        {
            duration.Property(value => value.StartDate).HasColumnName("start_date").HasColumnType("date").IsRequired();
            duration.Property(value => value.EndDate).HasColumnName("end_date").HasColumnType("date").IsRequired();
        });
        builder.Navigation(prescription => prescription.TreatmentPeriod).IsRequired();
        builder.OwnsOne(prescription => prescription.DosageLimit, dosage =>
        {
            dosage.Property(value => value.DailyQuantity).HasColumnName("daily_quantity").IsRequired();
            dosage.Property(value => value.ActiveIngredient).HasColumnName("active_ingredient").HasMaxLength(15).IsRequired();
        });
        builder.Navigation(prescription => prescription.DosageLimit).IsRequired();
        builder.Property(prescription => prescription.Status).HasConversion<string>().HasMaxLength(20).HasColumnName("status").IsRequired();
        builder.Property(prescription => prescription.ClinicalNotes).HasColumnName("clinical_notes").HasMaxLength(1000);
        builder.Property(prescription => prescription.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(prescription => prescription.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}
