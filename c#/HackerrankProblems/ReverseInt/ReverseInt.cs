using System.Text;

public class ReverseIntSolution
{
  public int Reverse(int x)
  {
    var isNegative = x < 0;
    var strValue = x.ToString().Substring(isNegative ? 1 : 0);
    var valueReversed = Reverse(strValue);
    if (IsNotValidValue(valueReversed, isNegative)) return 0;
    var result = int.Parse(valueReversed);
    if (isNegative) return -1 * result;
    return result;
  }

  private string Reverse(string s)
  {
    var stringBuilder = new StringBuilder();
    for (int i = s.Length - 1; i >= 0; i--)
    {
      stringBuilder.Append(s[i]);
    }
    return stringBuilder.ToString();
  }

  private bool IsNotValidValue(string s, bool isNegative)
  {
    if (isNegative)
    {
      var minStrValue = int.MinValue.ToString().Substring(1);
      return s.Length == minStrValue.Length && s.CompareTo(minStrValue) > 0;
    }
    var maxStrValue = int.MaxValue.ToString();
    return s.Length == maxStrValue.Length && s.CompareTo(maxStrValue) > 0;
  }
}