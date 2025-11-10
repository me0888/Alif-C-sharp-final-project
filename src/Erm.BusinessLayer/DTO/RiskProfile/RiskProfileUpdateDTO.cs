namespace Erm.BusinessLayer.DTO;

public readonly record struct RiskProfileUpdateDTO(
    string? Name,
    string? Description,
    int? BusinessProcessId,
    int? OccurrenceProbability,
    int? PotentialBusinessImpact
);

