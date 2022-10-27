class StackStabilizationSolution {
  
  public int getMinimumDeflatedDiscCount(int N, int[] R) {
    var count = 0;
    for (var i = N - 2; i >= 0; i--)
    {
      if (R[i] < R[i + 1]) continue;
      count++;
      R[i] = R[i + 1] - 1;
      if (R[i] <= 0) return -1;
    }
    return count;
  }
  
}
