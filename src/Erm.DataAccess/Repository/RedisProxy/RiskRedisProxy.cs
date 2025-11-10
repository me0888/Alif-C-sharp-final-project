using System.Text.Json;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Caching.Distributed;

using Microsoft.Extensions.Logging;

using Erm.DataAccess.Models;


namespace Erm.DataAccess.Repository;

public sealed class RiskRepositoryRedisProxy(
    IDistributedCache distributedCache,
    RiskRepository originalRepository,
    ILogger<RiskRepositoryRedisProxy> logger) : IRiskRepository
{
    private readonly IDistributedCache _db = distributedCache;
    private readonly RiskRepository _originalRepository = originalRepository;
    private readonly ILogger<RiskRepositoryRedisProxy> _logger = logger;

    public async Task<Risk> CreateAsync(Risk entity, CancellationToken token = default)
    {
        Risk risk = await _originalRepository.CreateAsync(entity, token);
        string riskKey = $"{KeyConts.Risk}{risk.RiskProfileId}";

        _db.Remove(riskKey);

        return risk;
    }

    public async Task<IEnumerable<Risk>> GetAllAsync(CancellationToken token = default)
		=> await _originalRepository.GetAllAsync(token);

    public async Task<Risk> GetAsync(int id, CancellationToken token = default)
    {
        string riskKey = $"{KeyConts.Risk}{id}";
        string? redisValue = await _db.GetStringAsync(riskKey);

        if (redisValue != null)
            redisValue = Regex.Unescape(redisValue);

        _logger.LogInformation($"\n ###  EXSTING Risk from RedisCache: {redisValue}\n");

        if (redisValue == null)
        {
            Risk profileFromDb = await _originalRepository.GetAsync(id, token);

            string redisProfileJson = JsonSerializer.Serialize(profileFromDb);

            await _db.SetStringAsync(riskKey, redisProfileJson, KeyOptions.RedisCacheOptions);

            _logger.LogInformation($"\n ###  NEW Risk save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

            return profileFromDb;
        }

        Risk profile = JsonSerializer.Deserialize<Risk>(redisValue)
            ?? throw new InvalidOperationException();
        return profile;

    }

    public async Task<Risk> UpdateAsync(int id, Risk Risk, CancellationToken token = default)
    {
        string riskKey = $"{KeyConts.Risk}{id}";
        Risk updatedProfile = await _originalRepository.UpdateAsync(id, Risk, token);

        string redisProfileJson = JsonSerializer.Serialize(updatedProfile);
        await _db.SetStringAsync(riskKey, redisProfileJson, KeyOptions.RedisCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED Risk save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

        return updatedProfile;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        int riskProfileId = await _originalRepository.DeleteAsync(id, token);
        string riskKey = $"{KeyConts.Risk}{id}";
        string riskProfileKey = $"{KeyConts.RiskProfile}{riskProfileId}";

        _db.Remove(riskKey);
        _db.Remove(riskProfileKey);

        return riskProfileId;
    }
}
