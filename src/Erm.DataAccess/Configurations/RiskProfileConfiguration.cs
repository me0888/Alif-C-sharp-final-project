using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erm.DataAccess.Models;
namespace Erm.DataAccess.Configurations;


public sealed class RiskProfileConfiguration : IEntityTypeConfiguration<RiskProfile>
{
    public void Configure(EntityTypeBuilder<RiskProfile> builder)
    {
        builder.ToTable("risk_profile");

        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(p => p.RiskName)
            .HasColumnName("risk_name")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .HasIndex(e => e.RiskName)
            .IsUnique();

        builder
            .Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("VARCHAR(500)")
            .IsRequired();

        builder
            .Property(p => p.OccurrenceProbability)
            .HasColumnName("occurrence_probability")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(p => p.PotentialBusinessImpact)
            .HasColumnName("potential_business_impact")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(p => p.PotentialSolution)
            .HasColumnName("potential_solution")
            .HasColumnType("VARCHAR(100)")
            .IsRequired(false);

        builder
            .Property(p => p.BusinessProcessId)
            .HasColumnName("business_process_id");

        builder
            .HasOne(r => r.BusinessProcess)
            .WithMany(b => b.RiskProfiles)
            .HasForeignKey(fk => fk.BusinessProcessId)
            .IsRequired();

        builder.HasKey(k => k.Id);
    }
}