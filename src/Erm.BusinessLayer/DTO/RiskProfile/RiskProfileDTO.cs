namespace Erm.BusinessLayer.DTO;

public readonly record struct RiskProfileDTO(
    int Id,
    string Name,
    string Description,
    int BusinessProcessId,
    int OccurrenceProbability,
    int PotentialBusinessImpact
);

