using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public interface IBusinessProcessRepository
{
    public Task<BusinessProcess> CreateAsync(BusinessProcess entity, CancellationToken token = default);
    public Task<BusinessProcess> GetAsync(int id, CancellationToken token = default);
    public Task<IEnumerable<BusinessProcess>> GetAllAsync(CancellationToken token = default);
    public Task<BusinessProcess> UpdateAsync(int id, BusinessProcess businessProcess, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
}