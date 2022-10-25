using System.Collections.Generic;

class CafeteriaSolution
{

  public long getMaxAdditionalDinersCount(long N, long K, int M, long[] S)
  {
    var subsets = FindSubsets(S, M, K, N);
    var result = 0L;
    foreach (var subsetLength in subsets)
    {
      result += subsetLength / (1 + K) + (subsetLength % (1 + K) > 0 ? 1 : 0);
    }

    return result;
  }

  private List<long> FindSubsets(long[] s, int m, long k, long n)
  {
    Array.Sort(s);
    var subsets = new List<long>();
    for (int i = 0; i < s.Length; i++)
    {
      if (i == 0)
      {
        var subsetLength = s[i] - 1 - k;
        if (subsetLength > 0) subsets.Add(subsetLength);
        var next = s.Length == 1 ? n : s[i + 1];
        var distanceToCheckNext = s.Length == 1 ? k : 1 + 2 * k;
        subsetLength = next - s[i] - distanceToCheckNext;
        if (subsetLength > 0) subsets.Add(subsetLength);
        continue;
      }
      if (i == s.Length - 1)
      {
        var subsetLength = n - s[i] - k;
        if (subsetLength > 0) subsets.Add(subsetLength);
        break;
      }
      var middleSubsetLength = s[i + 1] - s[i] - 1 - 2 * k;
      if (middleSubsetLength > 0) subsets.Add(middleSubsetLength);
    }
    return subsets;
  }
}