using System.Text.Json.Serialization;

namespace Erm.DataAccess.Models;

public sealed class RiskProfile
{
    public int Id { get; set; }
    public required string RiskName { get; set; }
    public string? Description { get; set; }
    public required int OccurrenceProbability { get; set; }
    public required int PotentialBusinessImpact { get; set; }
    public string? PotentialSolution { get; set; }
    public required int BusinessProcessId { get; set; }
    [JsonIgnore]
    public BusinessProcess? BusinessProcess { get; set; }

    public ICollection<Risk> Risks { get; set; } = null!;
    public ICollection<Notification> Notifications { get; set; } = null!;
}
