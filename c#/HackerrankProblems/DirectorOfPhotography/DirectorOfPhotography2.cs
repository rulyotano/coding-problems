
class DirectorOfPhotography2Solution
{
  private const char Actor = 'A';
  private const char Backdrop = 'B';
  private const char Photographer = 'P';

  public long getArtisticPhotographCount(int N, string C, int X, int Y)
  {
    C = C.ToUpper();
    var count = 0L;
    var i = X;
    var leftR = i - X;
    var leftL = i - Y;
    var rightL = i + X;
    var rightR = i + Y;
    var bLeft = C[leftR] == Backdrop ? 1 : 0;
    var pLeft = C[leftR] == Photographer ? 1 : 0;
    var (bRight, pRight) = CountPsBs(rightL, rightR, C);

    while (rightL < N)
    {
      if (C[i] == Actor)
      {
        count += (bLeft * (long)pRight + pLeft * (long)bRight);
      }
      if (leftL >= 0)
      {
        if (C[leftL] == Photographer) pLeft--;
        if (C[leftL] == Backdrop) bLeft--;
      }
      if (C[rightL] == Photographer) pRight--;
      if (C[rightL] == Backdrop) bRight--;

      i++; leftL++; leftR++; rightL++; rightR++;

      if (rightR < N)
      {
        if (C[rightR] == Photographer) pRight++;
        if (C[rightR] == Backdrop) bRight++;
      }
      if (C[leftR] == Photographer) pLeft++;
      if (C[leftR] == Backdrop) bLeft++;
    }

    return count;
  }

  private (int bs, int ps) CountPsBs(int rightL, int rightR, string C)
  {
    var bs = 0;
    var ps = 0;
    var maxRight = Math.Min(rightR, C.Length - 1);
    for (int i = Math.Min(rightL, C.Length - 1); i <= maxRight; i++)
    {
      if (C[i] == Backdrop) bs++;
      if (C[i] == Photographer) ps++;
    }
    return (bs, ps);
  }
}
