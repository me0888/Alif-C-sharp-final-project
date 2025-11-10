using Microsoft.EntityFrameworkCore;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class RiskProfileRepository(
    ErmDbContext dbContext) : IRiskProfileRepository
{
    private readonly ErmDbContext _db = dbContext;

    public async Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken token = default)
    {
        await _db.RiskProfiles.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);
        return entity;
    }

    public async Task<IEnumerable<RiskProfile>> GetAllAsync(CancellationToken token = default) 
        => await _db.RiskProfiles.AsNoTracking().OrderBy(r => r.Id).ToArrayAsync(token);

    public async Task<RiskProfile> GetAsync(int id, CancellationToken token = default)
    {
        RiskProfile riskProfile = await _db.RiskProfiles
                .AsNoTracking()
                .Include(r => r.Risks)
                .SingleAsync(x => x.Id == id, token);

        return riskProfile;
    }

    public async Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => await _db.RiskProfiles
            .AsNoTracking()
            .Include(r => r.Risks)
            .Where(x => x.RiskName
            .Contains(query) || (x.Description != null && x.Description.Contains(query))).ToArrayAsync(token);

    public async Task<RiskProfile> UpdateAsync(int id, RiskProfile updateRiskProfile, CancellationToken token = default)
    {
        RiskProfile profileToUpdate = await _db.RiskProfiles
            .Include(r => r.Risks)
            .SingleAsync(x => x.Id == id, token);

        if (updateRiskProfile.BusinessProcessId != 0)
            profileToUpdate.BusinessProcessId = updateRiskProfile.BusinessProcessId;

        if (!string.IsNullOrWhiteSpace(updateRiskProfile.RiskName))
            profileToUpdate.RiskName = updateRiskProfile.RiskName;

        if (!string.IsNullOrWhiteSpace(updateRiskProfile.Description))
            profileToUpdate.Description = updateRiskProfile.Description;

        if (updateRiskProfile.OccurrenceProbability != 0)
            profileToUpdate.OccurrenceProbability = updateRiskProfile.OccurrenceProbability;

        if (updateRiskProfile.PotentialBusinessImpact != 0)
            profileToUpdate.PotentialBusinessImpact = updateRiskProfile.PotentialBusinessImpact;

        if (!string.IsNullOrWhiteSpace(updateRiskProfile.PotentialSolution))
            profileToUpdate.PotentialSolution = updateRiskProfile.PotentialSolution;

        await _db.SaveChangesAsync(token);
        return profileToUpdate;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        RiskProfile riskProfile = await _db.RiskProfiles.AsNoTracking().SingleAsync(x => x.Id == id, token);
        await _db.RiskProfiles.Where(x => x.Id == id).ExecuteDeleteAsync(token);

        return riskProfile.BusinessProcessId;
    }
}
