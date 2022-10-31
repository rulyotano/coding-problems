class MissingMailSolution
{

  public double getMaxExpectedProfit(int N, int[] V, int C, double S)
  {
    var solveCache = new Dictionary<(int, int), double>();
    var previousCache = new Dictionary<(int, int), double>();

    double CalculatePrevious(int start, int end)
    {
      var key = (start, end);
      if (previousCache.ContainsKey(key)) return previousCache[key];

      var sum = 0D;
      for (int i = start; i <= end; i++)
      {
        var pow = end - i + 1;
        var probabilityOfHavingIt = 1 - S;
        sum += V[i] * Math.Pow(probabilityOfHavingIt, pow);
      }

      previousCache.Add(key, sum);
      return sum;
    }

    double Solve(int i, int j)
    {
      var key = (i, j);
      if (solveCache.ContainsKey(key)) return solveCache[key];

      if (i == N - 1)
      {
        var value = CalculatePrevious(j, i - 1) + V[i] - C;
        var result = Math.Max(0, value);
        solveCache.TryAdd(key, result);
        return result;
      }

      var resultIncluding = V[i] - C + CalculatePrevious(j, i - 1) + Solve(i + 1, i + 1);
      var resultNotIncluding = Solve(i + 1, j);

      var finalResult = Math.Max(resultIncluding, resultNotIncluding);
      solveCache.TryAdd(key, finalResult);
      return finalResult;
    }

    return Solve(0, 0);
  }

}
