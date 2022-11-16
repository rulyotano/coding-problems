class RotaryLockSolution2
{

  public long getMinCodeEntryTime(int N, int M, int[] C)
  {
    var memoization = new Dictionary<(int, int, int), long>();

    int DistanceBetween(int number1, int number2)
    {
      int OneDirectionDistance(int left, int right) => Math.Min(Math.Abs(left - right), Math.Abs(left - N) + right);

      return Math.Min(OneDirectionDistance(number1, number2), OneDirectionDistance(number2, number1));
    }

    long BestSolution(int i, int j, int k)
    {
      var key = (i, j, k);
      if (k < M - 1 && memoization.ContainsKey(key)) return memoization[key];

      if (k == M - 1)
      {
        return Math.Min(DistanceBetween(i, C[k]), DistanceBetween(j, C[k]));
      }

      var best = Math.Min(DistanceBetween(i, C[k]) + BestSolution(C[k], j, k + 1), DistanceBetween(j, C[k]) + BestSolution(i, C[k], k + 1));
      memoization.Add(key, best);
      return best;
    };

    return BestSolution(1, 1, 0);
  }

}