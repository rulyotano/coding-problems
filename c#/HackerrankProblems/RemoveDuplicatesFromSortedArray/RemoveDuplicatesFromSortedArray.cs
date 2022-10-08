public partial class Solution
{
  public int RemoveDuplicates(int[] nums)
  {
    if (nums.Length <= 1) return nums.Length;

    var i = 0;
    var j = 1;

    while (j < nums.Length)
    {
      while (nums[i] == nums[j] && j < nums.Length - 1)
      {
        j++;
      }
      i++;
      if (i != j)
      {
        nums[i] = nums[j];
      }
      j++;
    }
    return nums[i] == nums[i - 1] ? i : i + 1;
  }
}