
class KaitenzushiSolution
{

    public int getMaximumEatenDishCount(int N, int[] D, int K)
    {
        var previusEtan = new HashSet<int>();
        var previusEatenQueue = new Queue<int>();

        var count = 0;
        for (int i = 0; i < N; i++)
        {
            if (!previusEtan.Contains(D[i]))
            {
                previusEtan.Add(D[i]);
                previusEatenQueue.Enqueue(D[i]);
                count++;
                if (previusEtan.Count > K)
                {
                    var previousItemToRemove = previusEatenQueue.Dequeue();
                    previusEtan.Remove(previousItemToRemove);
                }
            }
        }

        return count;
    }

}
