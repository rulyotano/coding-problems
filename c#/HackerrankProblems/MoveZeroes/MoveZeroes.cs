public partial class Solution
{
  public void MoveZeroes(int[] nums)
  {
    var i = nums.Length - 2;
    while (i >= 0)
    {
      if (nums[i] == 0)
      {
        var next = i;
        while (next < nums.Length - 1 && nums[next + 1] != 0)
        {
          nums[next] = nums[next + 1];
          nums[next + 1] = 0;
          next++;
        }
      }
      i--;
    }
  }
}