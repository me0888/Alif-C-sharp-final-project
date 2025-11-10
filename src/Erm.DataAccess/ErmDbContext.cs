using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Erm.DataAccess.Models;

namespace Erm.DataAccess;

public sealed class ErmDbContext : DbContext
{
    public ErmDbContext() { }
    public ErmDbContext(DbContextOptions options) : base(options) { }
    public DbSet<BusinessProcess> BusinessProcesses { get; set; } = null!;
    public DbSet<RiskProfile> RiskProfiles { get; set; } = null!;
    public DbSet<Risk> Risks { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql(ConnectionString);    
}