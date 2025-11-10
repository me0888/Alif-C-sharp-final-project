using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class BusinessProcessRepositoryInMemoryProxy(
    IMemoryCache cache,
    BusinessProcessRepository originalRepository,
    ILogger<BusinessProcessRepositoryInMemoryProxy> logger) : IBusinessProcessRepository
{
    private readonly IMemoryCache _cache = cache;
    private readonly BusinessProcessRepository _originalRepository = originalRepository;
    private readonly ILogger<BusinessProcessRepositoryInMemoryProxy> _logger = logger;

    public Task<BusinessProcess> CreateAsync(BusinessProcess entity, CancellationToken token = default)
        => _originalRepository.CreateAsync(entity, token);

    public Task DeleteAsync(int id, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";

        _cache.Remove(businessProcessKey);
        return _originalRepository.DeleteAsync(id, token);
    }

    public Task<IEnumerable<BusinessProcess>> GetAllAsync(CancellationToken token = default)
        => _originalRepository.GetAllAsync(token);

    public async Task<BusinessProcess> GetAsync(int id, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";

        if (!_cache.TryGetValue(businessProcessKey, out BusinessProcess? businessProcess))
        {
            BusinessProcess businessProcessFromDb = await _originalRepository.GetAsync(id, token);

            _cache.Set(businessProcessKey, businessProcessFromDb, KeyOptions.MemoryCacheOptions);

            _logger.LogInformation($"\n ###  NEW BusinessProcess save to InMemoryCache: {businessProcessFromDb.Name}\n");

            return businessProcessFromDb;
        }

        _logger.LogInformation($"\n ###  EXSTING BusinessProcess from InMemoryCache: {businessProcess.Name}\n");

        return businessProcess;
    }

    public async Task<BusinessProcess> UpdateAsync(int id, BusinessProcess businessProcess, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";

        BusinessProcess updatedProfile = await _originalRepository.UpdateAsync(id, businessProcess, token);

        _cache.Set(businessProcessKey, updatedProfile, KeyOptions.MemoryCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED BusinessProcess save to InMemoryCache: {updatedProfile.Name}\n");

        return updatedProfile;
    }

}
