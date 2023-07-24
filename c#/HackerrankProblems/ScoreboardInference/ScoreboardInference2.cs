class ScoreboardInference1Solution2
{

  public int getMinProblemCount(int N, int[] S)
  {
    var count = 0;
    var hasMod1 = false;
    var hasMod2 = false;
    for (var i = 0; i < N; i++)
    {
      if (S[i] % 3 == 1)
      {
        if (!hasMod1)
        {
          hasMod1 = true;
          count++;
        }
        S[i]--;
      }
      else if (S[i] % 3 == 2)
      {
        if (!hasMod2)
        {
          hasMod2 = true;
          count++;
        }
        S[i] -= 2;
      }
    }
    var max = S.Max();
    if (max > 0 && hasMod1 && hasMod2)
      max -= 3;
    return count + (max / 3);
  }

}
