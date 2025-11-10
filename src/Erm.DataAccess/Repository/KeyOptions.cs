using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Erm.DataAccess.Repository;

public static class KeyOptions
{
    public readonly static DistributedCacheEntryOptions RedisCacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
    };

    public readonly static MemoryCacheEntryOptions MemoryCacheOptions = new MemoryCacheEntryOptions()
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));
}