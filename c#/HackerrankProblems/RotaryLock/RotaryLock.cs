class RotaryLockSolution {
  
  public long getMinCodeEntryTime(int N, int M, int[] C) {
    var result = 0L;
    var previous = 1;

    int DistanceBetween(int number1, int number2)
    {
      return Math.Min(
        Math.Abs(number1 - number2),
        Math.Abs(number1 - N) + number2
      );
    }

    for (int i = 0; i < M; i++)
    {
      result+=Math.Min(DistanceBetween(C[i], previous), DistanceBetween(previous, C[i]));
      previous = C[i];
    }

    return result;
  }
  
}