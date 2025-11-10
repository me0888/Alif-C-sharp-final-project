using Erm.DataAccess.Models;

namespace Erm.BusinessLayer.DTO;

public readonly record struct BusinessProcessDTO(
    int Id,
    string Name,
    string Domain
);

