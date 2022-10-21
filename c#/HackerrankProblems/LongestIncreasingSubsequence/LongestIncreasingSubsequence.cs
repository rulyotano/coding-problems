public class LongestIncreasingSubsequenceSolution
{
  public int LengthOfLIS(int[] nums)
  {
    var bestSequences = new int[nums.Length];
    for (int i = 0; i < nums.Length; i++)
    {
      bestSequences[i] = 1;
      for (int j = i - 1; j >= 0; j--)
      {
        if (nums[i] > nums[j])
          bestSequences[i] = Math.Max(bestSequences[i], 1 + bestSequences[j]);
      }
    }
    return bestSequences.Max();
  }
}