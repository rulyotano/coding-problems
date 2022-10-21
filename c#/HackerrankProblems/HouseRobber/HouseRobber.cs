public class HouseRobberSolution
{
  public int Rob(int[] nums)
  {
    var cache = new Dictionary<int, int>();
    int RobPick(int currentIndex)
    {
      if (cache.ContainsKey(currentIndex)) return cache[currentIndex];
      if (currentIndex >= nums.Length) return 0;
      if (currentIndex == nums.Length - 1) return nums[currentIndex];

      var result = Math.Max(nums[currentIndex] + RobPick(currentIndex + 2), RobPick(currentIndex + 1));
      cache.Add(currentIndex, result);
      return result;
    }

    return RobPick(0);
  }
}