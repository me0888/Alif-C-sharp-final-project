using System.Text.Json;

using System.Text.RegularExpressions;

using Microsoft.Extensions.Caching.Distributed;

using Microsoft.Extensions.Logging;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class RiskProfileRepositoryRedisProxy(
    IDistributedCache distributedCache,
    RiskProfileRepository originalRepository,
    ILogger<RiskProfileRepositoryRedisProxy> logger) : IRiskProfileRepository
{
    private readonly IDistributedCache _db = distributedCache;
    private readonly RiskProfileRepository _originalRepository = originalRepository;
    private readonly ILogger<RiskProfileRepositoryRedisProxy> _logger = logger;


    public async Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken token = default)
    {
        RiskProfile riskProfile = await _originalRepository.CreateAsync(entity, token);

        string businessProcessKey = $"{KeyConts.BusinessProcess}{riskProfile.BusinessProcessId}";
        _db.Remove(businessProcessKey);

        return riskProfile;
    }
	
	public async Task<IEnumerable<RiskProfile>> GetAllAsync(CancellationToken token = default)
        => await _originalRepository.GetAllAsync(token);
	
    public async Task<RiskProfile> GetAsync(int id, CancellationToken token = default)
    {
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";
        string? redisValue = await _db.GetStringAsync(riskProfileKey);

        if (redisValue != null)
            redisValue = Regex.Unescape(redisValue);

        _logger.LogInformation($"\n ###  EXSTING RiskProfile from RedisCache: {redisValue}\n");

        if (redisValue == null)
        {
            RiskProfile profileFromDb = await _originalRepository.GetAsync(id, token);

            // var options = new JsonSerializerOptions
            // {
            // ReferenceHandler = ReferenceHandler.Preserve
            // };
            // var redisProfileJson = JsonSerializer.Serialize(profileFromDb, options);

            string redisProfileJson = JsonSerializer.Serialize(profileFromDb);

            await _db.SetStringAsync(riskProfileKey, redisProfileJson, KeyOptions.RedisCacheOptions);

            _logger.LogInformation($"\n ###  NEW RiskProfile save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

            return profileFromDb;
        }

        RiskProfile profile = JsonSerializer.Deserialize<RiskProfile>(redisValue)
            ?? throw new InvalidOperationException();
        return profile;

    }

    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => _originalRepository.QueryAsync(query, token);

    public async Task<RiskProfile> UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default)
    {
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";

        RiskProfile updatedProfile = await _originalRepository.UpdateAsync(id, riskProfile, token);

        string redisProfileJson = JsonSerializer.Serialize(updatedProfile);

        await _db.SetStringAsync(riskProfileKey, redisProfileJson, KeyOptions.RedisCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED RiskProfile save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

        return updatedProfile;
    }
    
    public async Task<int> DeleteAsync(int id, CancellationToken token = default)
    {
        int busnessBrossesId = await _originalRepository.DeleteAsync(id, token);
        string riskProfileKey = $"{KeyConts.RiskProfile}{id}";
        string busnessBrossesKey = $"{KeyConts.BusinessProcess}{busnessBrossesId}";

        _db.Remove(riskProfileKey);
        _db.Remove(busnessBrossesKey);

        return busnessBrossesId;
    }
}
