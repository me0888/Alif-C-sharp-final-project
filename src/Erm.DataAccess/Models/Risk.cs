using System.Text.Json.Serialization;

namespace Erm.DataAccess.Models;

public sealed class Risk
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
    public required int TimeFrame { get; set; }
    public int? Status { get; set; }

    public int RiskProfileId { get; set; }
    [JsonIgnore]
    public RiskProfile? RiskProfiles { get; set; }

    public ICollection<Notification> Notifications { get; set; } = null!;
}
