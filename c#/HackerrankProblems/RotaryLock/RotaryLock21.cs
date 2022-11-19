class RotaryLockSolution21
{

  public long getMinCodeEntryTime(int N, int M, int[] C)
  {
    var memoization = new Dictionary<(int, int), long>();
    var c = new int[M + 1];
    c[0] = 1;
    for (int i = 0; i < M; i++)
    {
      c[i + 1] = C[i];
    }

    int DistanceBetween(int index1, int index2)
    {
      int OneDirectionDistance(int left, int right) => Math.Min(Math.Abs(left - right), Math.Abs(left - N) + right);

      var number1 = c[index1];
      var number2 = c[index2];
      return Math.Min(OneDirectionDistance(number1, number2), OneDirectionDistance(number2, number1));
    }

    long BestSolution(int i, int j)
    {
      var key = (i, j);
      var k = Math.Max(i, j) + 1;
      if (k < c.Length - 1 && memoization.ContainsKey(key)) return memoization[key];

      if (k == c.Length - 1)
      {
        return Math.Min(DistanceBetween(i, k), DistanceBetween(j, k));
      }

      var best = Math.Min(DistanceBetween(i, k) + BestSolution(k, j), DistanceBetween(j, k) + BestSolution(i, k));
      memoization.Add(key, best);
      return best;
    };

    return BestSolution(0, 0);
  }

}