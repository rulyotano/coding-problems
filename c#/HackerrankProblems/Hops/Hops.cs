class HopsSolution {
  
  public long getSecondsRequired(long N, int F, long[] P) {
    var firstFrog = N;
    for (var i = 0; i < F; i++)
    {
      firstFrog = Math.Min(firstFrog, P[i]);
    }
    return N - firstFrog;
  }
  
}