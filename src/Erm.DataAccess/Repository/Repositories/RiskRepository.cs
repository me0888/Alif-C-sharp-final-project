using Microsoft.EntityFrameworkCore;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed partial class RiskRepository(
    ErmDbContext dbContext) : IRiskRepository
{
    private readonly ErmDbContext _db = dbContext;

    public async Task<Risk> CreateAsync(Risk entity, CancellationToken token = default)
    {
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync(token);
        return entity;
    }

    public async Task<IEnumerable<Risk>> GetAllAsync(CancellationToken token = default)
        => await _db.Risks
        .AsNoTracking()
		.OrderBy(r => r.Id)
        .ToArrayAsync(token);

    public async Task<Risk> GetAsync(int id, CancellationToken token = default)
        => await _db.Risks
        .AsNoTracking()
        .SingleAsync(x => x.Id == id);

    public async Task<Risk> UpdateAsync(int id, Risk risk, CancellationToken token = default)
    {
        Risk riskToUpdate = await _db.Risks.SingleAsync(x => x.Id == id);

        if (risk.RiskProfileId != 0)
            riskToUpdate.RiskProfileId = risk.RiskProfileId;

        if (!string.IsNullOrWhiteSpace(risk.Name))
            riskToUpdate.Name = risk.Name;

        if (!string.IsNullOrWhiteSpace(risk.Description))
            riskToUpdate.Description = risk.Description;

        if (!string.IsNullOrWhiteSpace(risk.Type))
            riskToUpdate.Type = risk.Type;

        if (risk.TimeFrame != 0)
            riskToUpdate.TimeFrame = risk.TimeFrame;

        if (risk.Status != null)
            riskToUpdate.Status = risk.Status;

        await _db.SaveChangesAsync(token);
        return riskToUpdate;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        Risk riskProfile = await _db.Risks.AsNoTracking().SingleAsync(x => x.Id == id);

        await _db.Risks.Where(x => x.Id == id).ExecuteDeleteAsync(token);

        return riskProfile.RiskProfileId;
    }
}