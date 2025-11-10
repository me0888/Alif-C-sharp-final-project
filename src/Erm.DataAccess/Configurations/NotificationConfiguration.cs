using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erm.DataAccess.Models;

namespace Erm.DataAccess.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notification");

        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("INTEGER")
            .IsRequired();

        builder
            .Property(p => p.BusinessProcessId)
            .HasColumnName("business_process_id")
            .HasColumnType("INTEGER")
            .IsRequired(false);

        builder
            .Property(p => p.RiskId)
            .HasColumnName("risk_id")
            .HasColumnType("INTEGER")
            .IsRequired(false);

        builder
            .Property(p => p.RiskProfileId)
            .HasColumnName("risk_profile_id")
            .HasColumnType("INTEGER")
            .IsRequired(false);

        builder
            .Property(p => p.Message)
            .HasColumnName("message")
            .HasColumnType("VARCHAR(500)")
            .IsRequired();

        builder
            .Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder
            .Property(p => p.IsRead)
            .HasColumnName("is_read")
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .HasOne(x => x.BusinessProcess)
            .WithMany(x => x.Notifications)
            .HasForeignKey(fk => fk.BusinessProcessId)
            .IsRequired();

        builder
            .HasOne(x => x.RiskProfile)
            .WithMany(x => x.Notifications)
            .HasForeignKey(fk => fk.RiskProfileId)
            .IsRequired();

        builder
            .HasOne(rp => rp.Risk)
            .WithMany(r => r.Notifications)
            .HasForeignKey(fk => fk.RiskId)
            .IsRequired();

        builder.HasKey(k => k.Id);
    }
}
