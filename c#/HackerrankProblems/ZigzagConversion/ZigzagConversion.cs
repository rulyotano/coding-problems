using System.Text;

public class ZigzagConversionSolution
{
  public string Convert(string s, int numRows)
  {
    var n = s.Length;
    if (numRows == 1) return s;

    var strings = new StringBuilder[numRows];
    for (int i = 0; i < numRows; i++)
    {
      strings[i] = new StringBuilder();
    }

    strings[0].Append(s[0]);
    var direction = 1;
    for (int i = 1, k = 1; i < n; i++, k+=direction)
    {
      strings[k].Append(s[i]);
      if (i % (numRows - 1) == 0) direction *= -1;
    }

    return string.Concat(strings.Select(it => it.ToString()));
  }
}