using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using Erm.DataAccess.Models;

namespace Erm.DataAccess.Repository;

public sealed class BusinessProcessRepositoryRedisProxy(
    IDistributedCache distributedCache,
    BusinessProcessRepository originalRepository,
    ILogger<BusinessProcessRepositoryRedisProxy> logger) : IBusinessProcessRepository
{
    private readonly IDistributedCache _db = distributedCache;
    private readonly BusinessProcessRepository _originalRepository = originalRepository;
    private readonly ILogger<BusinessProcessRepositoryRedisProxy> _logger = logger;

    public Task<BusinessProcess> CreateAsync(BusinessProcess entity, CancellationToken token = default)
        => _originalRepository.CreateAsync(entity, token);

    public Task<IEnumerable<BusinessProcess>> GetAllAsync(CancellationToken token = default)
        => _originalRepository.GetAllAsync(token);

    public async Task<BusinessProcess> GetAsync(int id, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";
        string? redisValue = await _db.GetStringAsync(businessProcessKey);

        if (redisValue != null)
            redisValue = Regex.Unescape(redisValue);

        _logger.LogInformation($"\n ###  EXSTING BusinessProcess from RedisCache: {redisValue}\n");

        if (redisValue == null)
        {
            BusinessProcess profileFromDb = await _originalRepository.GetAsync(id, token);

            string redisProfileJson = JsonSerializer.Serialize(profileFromDb);

            await _db.SetStringAsync(businessProcessKey, redisProfileJson, KeyOptions.RedisCacheOptions);

            _logger.LogInformation($"\n ###  NEW BusinessProcess save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

            return profileFromDb;
        }

        BusinessProcess profile = JsonSerializer.Deserialize<BusinessProcess>(redisValue)
            ?? throw new InvalidOperationException();
        return profile;

    }

    public async Task<BusinessProcess> UpdateAsync(int id, BusinessProcess businessProcess, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";

        BusinessProcess updatedProfile = await _originalRepository.UpdateAsync(id, businessProcess, token);

        string redisProfileJson = JsonSerializer.Serialize(updatedProfile);
        await _db.SetStringAsync(businessProcessKey, redisProfileJson, KeyOptions.RedisCacheOptions);

        _logger.LogInformation($"\n ###  UPDATED BusinessProcess save to RedisCache: {Regex.Unescape(redisProfileJson)}\n");

        return updatedProfile;
    }

    public Task DeleteAsync(int id, CancellationToken token = default)
    {
        string businessProcessKey = $"{KeyConts.BusinessProcess}{id}";

        _db.Remove(businessProcessKey);
        return _originalRepository.DeleteAsync(id, token);
    }
}
