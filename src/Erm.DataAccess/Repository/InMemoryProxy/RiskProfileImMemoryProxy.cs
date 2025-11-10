using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class RiskProfileRepositoryImMemoryProxy(
    IMemoryCache cache,
    RiskProfileRepository originalRepository,
    ILogger<RiskProfileRepositoryImMemoryProxy> logger) : IRiskProfileRepository
{
    private readonly IMemoryCache _cache = cache;
    private readonly RiskProfileRepository _originalRepository = originalRepository;
    private readonly ILogger<RiskProfileRepositoryImMemoryProxy> _logger = logger;


    public async Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken token = default)
    {
        RiskProfile riskProfile = await _originalRepository.CreateAsync(entity, token);

        string businessProcessKey = $"{KeyConts.BusinessProcess}{riskProfile.BusinessProcessId}";
        _cache.Remove(businessProcessKey);

        return riskProfile;
    }

	public async Task<IEnumerable<RiskProfile>> GetAllAsync(CancellationToken token = default)
        => await _originalRepository.GetAllAsync(token);
	
    public async Task<RiskProfile> GetAsync(int id, CancellationToken token = default)
    {
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";

        if (!_cache.TryGetValue(riskProfileKey, out RiskProfile? profile))
        {
            RiskProfile profileFromDb = await _originalRepository.GetAsync(id, token);

            _cache.Set(riskProfileKey, profileFromDb, KeyOptions.MemoryCacheOptions);

            _logger.LogInformation($"\n ###  NEW RiskProfile save to InMemoryCache: {profileFromDb.RiskName}\n");

            return profileFromDb;
        }

        _logger.LogInformation($"\n ###  EXSTING RiskProfile from InMemoryCache: {profile.RiskName}\n");

        return profile;

    }

    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => _originalRepository.QueryAsync(query, token);

    public async Task<RiskProfile> UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default)
    {
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";

        RiskProfile updatedProfile = await _originalRepository.UpdateAsync(id, riskProfile, token);

        _cache.Set(riskProfileKey, updatedProfile, KeyOptions.MemoryCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED RiskProfile save to InMemoryCache: {updatedProfile.RiskName}\n");

        return updatedProfile;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        int busnessBrossesId = await _originalRepository.DeleteAsync(id, token);
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";
        string busnessBrossesKey = $"{KeyConts.BusinessProcess}{busnessBrossesId}";

        _cache.Remove(riskProfileKey);
        _cache.Remove(busnessBrossesKey);

        return busnessBrossesId;
    }
	
}
