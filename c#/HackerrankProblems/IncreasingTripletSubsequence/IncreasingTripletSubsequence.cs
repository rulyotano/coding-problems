public partial class Solution {
    public bool IncreasingTriplet(int[] nums) {
      var minMaxAllowed = int.MaxValue;
      var minFound = int.MaxValue;

      for (int i = 0; i < nums.Length; i++)
      {
        if (nums[i] > minMaxAllowed) return true;
        if (nums[i] > minFound && nums[i] < minMaxAllowed) minMaxAllowed = nums[i];
        minFound = Math.Min(minFound, nums[i]);
      }

      return false;
    }
}