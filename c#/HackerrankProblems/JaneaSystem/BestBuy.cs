namespace JaneaSystems;

public static partial class Solution
{
  public static int FindBestGain(int[] prices)
  {
    if (prices.Length < 2) return 0;
    var bestGain = 0;

    var currentBuy = prices[0];
    var nextBuyIndex = 1;
    while(nextBuyIndex < prices.Length)
    {
      var currentSell = prices[nextBuyIndex];
      if (currentSell >= currentBuy)
      {
        bestGain = Math.Max(bestGain, currentSell - currentBuy);
      }
      else
      {
        currentBuy = currentSell;
      }
      nextBuyIndex++;
    }

    return bestGain;
  }
}