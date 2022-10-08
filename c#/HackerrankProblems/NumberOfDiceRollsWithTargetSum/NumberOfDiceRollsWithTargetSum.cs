public partial class Solution
{
  private const int Module = 1000000000 + 7;
  public int NumRollsToTarget(int n, int k, int target)
  {
    var cache = new Dictionary<CacheKey, int>();

    int Result(int currentN, int currentTarget)
    {
      if (currentTarget <= k && currentTarget >= 1 && currentN == 1)
      {
        cache.Add(new CacheKey(currentN, currentTarget), 1);
        return 1;
      }
      if (currentTarget <= 0 || currentN <= 1)
      {
        cache.Add(new CacheKey(currentN, currentTarget), 0);
        return 0;
      }
      int result = 0;
      for (int i = 1; i <= k; i++)
      {
        var key = new CacheKey(currentN - 1, currentTarget - i);
        result = GetModule(result + ResultCached(key));
      }
      cache.Add(new CacheKey(currentN, currentTarget), result);
      return result;
    }

    int GetModule(int value)
    {
      return value % Module;
    }

    int ResultCached(CacheKey key)
    {
      if (cache.TryGetValue(key, out int cachedResult))
      {
        return cachedResult;
      }
      return Result(key.N, key.Target);
    }

    return Result(n, target);
  }
}

public record CacheKey(int N, int Target);