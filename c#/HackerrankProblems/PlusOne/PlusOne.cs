public partial class Solution {
    public int[] PlusOne(int[] digits) {
      var result = new List<int>();
      var hasCarraige = true;        
      for (int i = digits.Length - 1; i >= 0; i--)
      {
        var currentSum = digits[i] + (hasCarraige ? 1 : 0);
        result.Add(currentSum % 10);
        hasCarraige = currentSum == 10;
      }
      if (hasCarraige) result.Add(1);

      result.Reverse();
      return result.ToArray();
    }
}