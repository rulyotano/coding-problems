public partial class Solution {
    public bool ContainsDuplicate(int[] nums) {
      Array.Sort(nums);
      for (int i = 1; i < nums.Length; i++)
      {
        if (nums[i] == nums[i-1]) return true;
      }
      return false;
    }
}