using System.Text;

namespace JaneaSystems;

public static partial class Solution
{
  public static void PrintRepetitions(char[] charset, int k)
  {
    PrintRepetitionsRec(new StringBuilder(), charset, k, -1, 0);
  }

  private static void PrintRepetitionsRec(StringBuilder previousString, char[] charset, int k, int previousCharIndex, int currentIndex)
  {
    if (currentIndex >= k)
    {
      Console.WriteLine(previousString.ToString());
      return;
    }
    for (int i = previousCharIndex + 1; i < charset.Length; i++)
    {
      previousString.Append(charset[i]);
      PrintRepetitionsRec(previousString, charset, k, i, currentIndex + 1);
      previousString.Remove(previousString.Length - 1, 1);
    }
  }
}