namespace Erm.BusinessLayer.DTO;

public readonly record struct RiskCreateDTO(
    int RiskProfileId,
    string Name,
    string Type,
    string Description,
    int TimeFrame,
    int Status
);

