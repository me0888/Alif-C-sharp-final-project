using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public interface IRiskProfileRepository
{
    public Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken token = default);
	public Task<IEnumerable<RiskProfile>> GetAllAsync(CancellationToken token = default); 
    public Task<RiskProfile> GetAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default);
    public Task<RiskProfile> UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default);
    public Task<int> DeleteAsync(int id, CancellationToken token = default);
}


