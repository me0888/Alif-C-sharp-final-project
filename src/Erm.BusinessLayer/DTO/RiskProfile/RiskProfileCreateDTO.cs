namespace Erm.BusinessLayer.DTO;

public readonly record struct RiskProfileCreateDTO(
    string Name,
    string Description,
    int BusinessProcessId,
    int OccurrenceProbability,
    int PotentialBusinessImpact
);

