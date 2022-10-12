public partial class Solution
{
  public int[] TwoSum(int[] nums, int target)
  {
    var hashTable = new Dictionary<int, int>();
    for (int i = 0; i < nums.Length; i++)
    {
      var it = nums[i];
      if (hashTable.TryGetValue(it, out int existingIndex))
      {
        return new int[] {existingIndex, i};
      }
      hashTable.TryAdd(target - it, i);
    }
    throw new Exception("Invalid test case");
  }
}