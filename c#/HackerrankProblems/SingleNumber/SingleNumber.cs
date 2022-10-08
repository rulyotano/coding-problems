public partial class Solution
{
  public int SingleNumber(int[] nums)
  {
    var result = 0;
    foreach (var number in nums)
    {
      result = result ^ number;
    }
    return result;
  }
}