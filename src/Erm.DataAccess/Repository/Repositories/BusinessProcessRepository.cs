using Microsoft.EntityFrameworkCore;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class BusinessProcessRepository(
    ErmDbContext dbContext) : IBusinessProcessRepository
{
    private readonly ErmDbContext _db = dbContext;

    public async Task<BusinessProcess> CreateAsync(BusinessProcess entity, CancellationToken token = default)
    {
        await _db.BusinessProcesses.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);
        return entity;
    }

	public async Task<IEnumerable<BusinessProcess>> GetAllAsync(CancellationToken token = default)
    {
        IEnumerable<BusinessProcess> businessProcesses = await _db.BusinessProcesses
                .AsNoTracking()
                .OrderBy(r => r.Id)
                .ToArrayAsync(token);

        return businessProcesses;
    }

    public async Task<BusinessProcess> GetAsync(int id, CancellationToken token = default)
    {
        BusinessProcess businessProcess = await _db.BusinessProcesses
                .AsNoTracking()
                .Include(r => r.RiskProfiles)
                .OrderBy(r => r.Id)
                .SingleAsync(x => x.Id == id, token);

        return businessProcess;
    }

    public async Task<BusinessProcess> UpdateAsync(int id, BusinessProcess updateBusinessProcess, CancellationToken token = default)
    {
        BusinessProcess profileToUpdate = await _db.BusinessProcesses
                .Include(r => r.RiskProfiles)
                .SingleAsync(x => x.Id == id, token);

        if (!string.IsNullOrWhiteSpace(updateBusinessProcess.Name))
            profileToUpdate.Name = updateBusinessProcess.Name;

        if (!string.IsNullOrWhiteSpace(updateBusinessProcess.Domain))
            profileToUpdate.Domain = updateBusinessProcess.Domain;

        await _db.SaveChangesAsync(token);
        return profileToUpdate;
    }
    
    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        await _db.BusinessProcesses
            .AsNoTracking().SingleAsync(x => x.Id == id, token);

        await _db.BusinessProcesses
            .Where(x => x.Id == id).ExecuteDeleteAsync(token);
    }
}