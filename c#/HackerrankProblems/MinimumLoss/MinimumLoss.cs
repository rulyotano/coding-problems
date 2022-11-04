class MinimumLossResult
{

    /*
     * Complete the 'minimumLoss' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts LONG_INTEGER_ARRAY price as parameter.
     */

    public static int minimumLoss(List<long> price)
    {
        var minimumLoss = long.MaxValue;
        var alreadyInterrogated = new SortedSet<long>();

        for (int i = 0; i < price.Count; i++)
        {
            var alreadyInsertedGreaterThanMe = alreadyInterrogated.GetViewBetween(price[i] + 1, long.MaxValue);
            var closestGreaterValue = alreadyInsertedGreaterThanMe.FirstOrDefault();
            if (closestGreaterValue != default(long))
            {
                minimumLoss = Math.Min(minimumLoss, closestGreaterValue - price[i]);
            }
            alreadyInterrogated.Add(price[i]);
        }

        return (int) minimumLoss;

    }

}