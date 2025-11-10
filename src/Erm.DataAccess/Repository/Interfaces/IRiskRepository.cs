using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public interface IRiskRepository
{
    public Task<Risk> CreateAsync(Risk entity, CancellationToken token = default);
	public Task<IEnumerable<Risk>> GetAllAsync(CancellationToken token = default);
    public Task<Risk> GetAsync(int id, CancellationToken token = default);
    public Task<Risk> UpdateAsync(int id, Risk risk, CancellationToken token = default);
    public Task<int> DeleteAsync(int id, CancellationToken token = default);

}


