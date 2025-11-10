namespace Erm.DataAccess.Models;

public class BusinessProcess
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public ICollection<RiskProfile> RiskProfiles { get; set; } = null!;
    public ICollection<Notification> Notifications { get; set; } = null!;
}
