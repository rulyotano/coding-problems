public partial class Solution {
    public void Rotate(int[] nums, int k) {
      var rotate = k % nums.Length;
      var result = new int[nums.Length];

      for (int numsIndex = 0; numsIndex < nums.Length; numsIndex++)
      {
        var newIndex = (numsIndex + rotate) % nums.Length;
        result[newIndex] = nums[numsIndex];
      }

      for (int newIndex = 0; newIndex < result.Length; newIndex++)
      {
        nums[newIndex] = result[newIndex];
      }
    }
}