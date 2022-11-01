class MissingMailSolution2
{

  public double getMaxExpectedProfit2(int N, int[] V, int C, double S)
  {
    var arr = new double[2, N];
    var previousCalculations = new double[N, N];

    // calculate previous
    var probabilityOfHavingIt = 1 - S;
    for (int i = 0; i < N; i++)
    {
      var sum = V[i] * probabilityOfHavingIt;
      previousCalculations[i, i] = sum;
      for (int j = i + 1; j < N; j++)
      {
        sum = probabilityOfHavingIt * (V[j] + sum);
        previousCalculations[i, j] = sum;
      }
    }

    // initialize
    arr[0, N - 1] = Math.Max(0, V[N - 1] - C);
    for (int i = N - 2; i >= 0; i--)
    {
      arr[0, i] = Math.Max(0, previousCalculations[i, N - 2] + V[N - 1] - C);
    }

    // calculate
    var previousIndex = 0;
    for (int i = N - 2; i >= 0; i--)
    {
      var currentIndex = (previousIndex + 1) % 2;
      for (int j = i; j >= 0; j--)
      {
        var probabilitiesJtoI = j < i ? previousCalculations[j, i - 1] : 0;
        var resultIncluding = V[i] - C + probabilitiesJtoI + arr[previousIndex, i + 1];
        var resultNotIncluding = arr[previousIndex, j];
        arr[currentIndex, j] = Math.Max(resultIncluding, resultNotIncluding);
      }
      previousIndex = currentIndex;
    }

    return arr[previousIndex, 0];
  }

}
