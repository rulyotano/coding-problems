class UniformIntegersSolution
{

  public int getUniformIntegerCountInInterval(long A, long B)
  {
    var allValidNumbers = GenerateAllNumbers();
    allValidNumbers.Add(1111111111111L);
    var aIndex = -1;
    var bIndex = -1;
    for (int i = 0; i < allValidNumbers.Count; i++)
    {
      if (allValidNumbers[i] < A) continue;
      aIndex = i;
      break;
    }
    for (int i = allValidNumbers.Count - 1; i >= 0; i--)
    {
      if (B < allValidNumbers[i]) continue;
      bIndex = i;
      break;
    }
    if (aIndex > bIndex) return 0;
    return bIndex - aIndex + 1;
  }

  private List<long> GenerateAllNumbers()
  {
    var result = new List<long>();
    const int maxPower = 12;
    for (int i = 1; i <= maxPower; i++)
    {
      var ones = GenerateOnes(i);
      result.Add(ones);
      for (int j = 2; j <= 9; j++)
      {
        result.Add(ones * j);
      }
    }
    return result;
  }

  private long GenerateOnes(int i)
  {
    var ones = 1L;
    while (i > 1)
    {
      i--;
      ones = ones * 10 + 1;
    }
    return ones;
  }
}