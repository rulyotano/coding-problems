public partial class Solution
{
  public int MaxProfit(int[] prices)
  {
    var i = 0;
    var j = 1;
    var currentSum = 0;
    while (j < prices.Length)
    {
      while (j < prices.Length && prices[j] <= prices[i])
      {
        i++;j++;
      }
      while (j < prices.Length - 1 && prices[j + 1] > prices[j])
      {
        j++;
      }
      if (j < prices.Length)
      {
        currentSum += prices[j] - prices[i];
        i = j;
        j++;
      }
    }

    return currentSum;
  }
}