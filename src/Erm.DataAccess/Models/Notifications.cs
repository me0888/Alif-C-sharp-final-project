using System.Text.Json.Serialization;
namespace Erm.DataAccess.Models;

public sealed class Notification
{
    public int Id { get; set; }
    public int? RiskId { get; set; }
    public int? RiskProfileId { get; set; }
    public int? BusinessProcessId { get; set; }
    public required string Message { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required bool IsRead { get; set; }
    [JsonIgnore]
    public BusinessProcess BusinessProcess { get; set; } = null!;
    [JsonIgnore]
    public RiskProfile RiskProfile { get; set; } = null!;
    [JsonIgnore]
    public Risk Risk { get; set; } = null!;
}