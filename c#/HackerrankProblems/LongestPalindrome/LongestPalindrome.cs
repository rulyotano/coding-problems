public class LongestPalindromeSolution
{
  private const int NotFound = -1;
  public string LongestPalindrome(string s)
  {
    var besti = 0;
    var bestj = 0;
    int len(int i, int j) => j - i + 1;
    for (int i = 0; i < s.Length; i++)
    {
      var subSolution = FindGreatestSubPal(s, i - 1, i + 1);
      if (subSolution.i != NotFound && len(besti, bestj) < len(subSolution.i, subSolution.j))
      {
        besti = subSolution.i;
        bestj = subSolution.j;
      }
      if (i < s.Length - 1 && s[i] == s[i + 1])
      {
        if (len(besti, bestj) < 2)
        {
          besti = i;
          bestj = i + 1;
        }
        subSolution = FindGreatestSubPal(s, i - 1, i + 2);
        if (subSolution.i != NotFound && len(besti, bestj) < len(subSolution.i, subSolution.j))
        {
          besti = subSolution.i;
          bestj = subSolution.j;
        }
      }
    }
    return s.Substring(besti, len(besti, bestj));
  }

  private (int i, int j) FindGreatestSubPal(string s, int i, int j)
  {
    var si = NotFound;
    var sj = NotFound;

    while (i >= 0 && j < s.Length && s[i] == s[j])
    {
      si = i;
      sj = j;
      i--;
      j++;
    }

    return (si, sj);
  }
}