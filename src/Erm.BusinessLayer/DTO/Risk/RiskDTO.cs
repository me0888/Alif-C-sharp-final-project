namespace Erm.BusinessLayer.DTO;

public readonly record struct RiskDTO(
    int Id,
    int RiskProfileId,
    string Name,
    string Type,
    string Description,
    int TimeFrame,
    int Status
);

