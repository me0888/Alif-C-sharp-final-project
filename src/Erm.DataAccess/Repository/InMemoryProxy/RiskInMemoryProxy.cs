using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class RiskRepositoryInMemoryProxy(
    IMemoryCache cache,
    RiskRepository originalRepository,
    ILogger<RiskRepositoryInMemoryProxy> logger) : IRiskRepository
{
    private readonly IMemoryCache _cache = cache;
    private readonly RiskRepository _originalRepository = originalRepository;
    private readonly ILogger<RiskRepositoryInMemoryProxy> _logger = logger;

    public async Task<Risk> CreateAsync(Risk entity, CancellationToken token = default)
    {
        Risk risk = await _originalRepository.CreateAsync(entity, token);
        string riskProfileKey = $"{KeyConts.RiskProfile}{risk.RiskProfileId}";

        _cache.Remove(riskProfileKey);

        return risk;
    }
	
	public async Task<IEnumerable<Risk>> GetAllAsync(CancellationToken token = default)
		=> await _originalRepository.GetAllAsync(token);


    public async Task<Risk> GetAsync(int id, CancellationToken token = default)
    {
        string riskKey = $"{KeyConts.Risk}{id}";

        if (!_cache.TryGetValue(riskKey, out Risk? risk))
        {
            Risk riskFromDb = await _originalRepository.GetAsync(id, token);

            _cache.Set(riskKey, riskFromDb, KeyOptions.MemoryCacheOptions);

            _logger.LogInformation($"\n ###  NEW Risk save to InMemoryCache: {riskFromDb.Name}\n");

            return riskFromDb;
        }

        _logger.LogInformation($"\n ###  EXSTING Risk from InMemoryCache: {risk.Name}\n");

        return risk;

    }

    public async Task<Risk> UpdateAsync(int id, Risk risk, CancellationToken token = default)
    {
        string key = $"{KeyConts.Risk}{id}";
        Risk updatedProfile = await _originalRepository.UpdateAsync(id, risk, token);

        _cache.Set(key, updatedProfile, KeyOptions.MemoryCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED Risk save to InMemoryCache: {updatedProfile.Name}\n");

        return updatedProfile;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        int riskProfileId = await _originalRepository.DeleteAsync(id, token);
        string riskKey = $"{KeyConts.Risk}{id}";
        string riskProfileKey = $"{KeyConts.RiskProfile}{riskProfileId}";

        _cache.Remove(riskKey);
        _cache.Remove(riskProfileKey);

        return riskProfileId;
    }
}
