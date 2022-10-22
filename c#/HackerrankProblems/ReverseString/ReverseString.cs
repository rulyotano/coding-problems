public class ReverseStringSolution
{
  public void ReverseString(char[] s)
  {
    var n = s.Length;
    for (int i = 0; i < n / 2; i++)
    {
      var otherIndex = n - 1 - i;
      var t = s[i];
      s[i] = s[otherIndex];
      s[otherIndex] = t;
    }
  }
}