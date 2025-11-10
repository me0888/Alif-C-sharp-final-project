using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erm.DataAccess.Models;

namespace Erm.DataAccess.Configurations;

public sealed class BusinessProcessConfiguration : IEntityTypeConfiguration<BusinessProcess>
{
    public void Configure(EntityTypeBuilder<BusinessProcess> builder)
    {
        builder.ToTable("business_process");

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(50)")
            .IsUnicode()
            .IsRequired();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();

        builder
            .Property(p => p.Domain)
            .HasColumnName("domain")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .HasMany(r => r.RiskProfiles)
            .WithOne(b => b.BusinessProcess)
            .HasForeignKey(fk => fk.BusinessProcessId)
            .IsRequired();

        builder.HasKey(k => k.Id);
    }
}