public class LengthOfLongestSubstringSolution
{
  public int LengthOfLongestSubstring(string s)
  {
    var best = 0;
    var i = 0;
    var j = 0;

    while (i < s.Length)
    {
      var dict = new Dictionary<char, int> { { s[i], i } };
      for (j = i + 1; j < s.Length; j++)
      {
        if (dict.ContainsKey(s[j]))
        {
          best = Math.Max(best, j-i);
          i = dict[s[j]] + 1;
          break;
        }
        else
        {
          dict.Add(s[j], j);
        }
      }
      if (j == s.Length)
      {
        best = Math.Max(best, j - i);
        break;
      }
    }

    return best;
  }
}