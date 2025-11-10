using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erm.DataAccess.Models;

namespace Erm.DataAccess.Configurations;


public sealed class RiskConfiguration : IEntityTypeConfiguration<Risk>
{
    public void Configure(EntityTypeBuilder<Risk> builder)
    {
        builder.ToTable("risk");

        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();

        builder
            .Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("VARCHAR(500)")
            .IsRequired();

        builder
            .Property(p => p.Type)
            .HasColumnName("type")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .Property(p => p.TimeFrame)
            .HasColumnName("time_frame")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(p => p.Status)
            .HasColumnName("status")
            .HasColumnType("INTEGER")
            .IsRequired(false);

        builder
            .Property(p => p.RiskProfileId)
            .HasColumnName("risk_profile_id");

        builder
            .HasOne(rp => rp.RiskProfiles)
            .WithMany(r => r.Risks)
            .HasForeignKey(fk => fk.RiskProfileId)
            .IsRequired();

        builder.HasKey(k => k.Id);
    }
}
