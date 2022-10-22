public class FirstUniqueCharacterInStringSolution
{
  public int FirstUniqChar(string s)
  {
    var firstOccurHash = 0;
    var secondOccurHash = 0;
    for (int i = 0; i < s.Length; i++)
    {
        var charIndex = s[i] - 'a';
        var bitMask = 1 << charIndex;
        if ((secondOccurHash & bitMask) == bitMask)
          continue;
        if ((firstOccurHash & bitMask) == bitMask)
        {
          secondOccurHash |= bitMask;
          continue;
        }
        firstOccurHash |= bitMask;
    }

    for (int i = 0; i < s.Length; i++)
    {
        var charIndex = s[i] - 'a';
        var bitMask = 1 << charIndex;
        if ((secondOccurHash & bitMask) == 0 && (firstOccurHash & bitMask) == bitMask) return i;
    }
    return -1;
  }
}