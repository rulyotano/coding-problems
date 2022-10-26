class ScoreboardInference1Solution {
  
  public int getMinProblemCount(int N, int[] S) {
    var maxEven = 0;
    var hasOdd = false;
    for (var i = 0; i < N; i++)
    {
      var v = S[i];
      if (v % 2 == 1)
      {
        hasOdd = true;
        v--;
      }
      maxEven = Math.Max(maxEven, v);
    }
    return maxEven / 2 + (hasOdd ? 1 : 0);
  }
  
}
