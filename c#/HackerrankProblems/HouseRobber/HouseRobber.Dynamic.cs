public class HouseRobberSolutionDynamic
{
  public int Rob(int[] nums)
  {
    if (nums.Length == 0) return 0;
    if (nums.Length == 1) return nums[0];
    var previousBestResults = new int[2] { 0, nums[0] };

    for (int i = 1; i < nums.Length; i++)
    {
      var t = previousBestResults[1];
      previousBestResults[1] = Math.Max(nums[i] + previousBestResults[0], previousBestResults[1]);
      previousBestResults[0] = t;
    }

    return previousBestResults[1];
  }
}