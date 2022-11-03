class RabbitHole1RabbitHole1Solution
{

  public int getMaxVisitableWebpages(int N, int[] L)
  {
    var best = new int[N];
    var mark = new bool[N];

    for (int i = 0; i < N; i++)
    {
      if (mark[i]) continue;

      var nextCount = 1;
      best[i] = nextCount++;
      var stack = new Stack<int>();
      stack.Push(i);
      var j = L[i] - 1;
      while (best[j] == 0)
      {
        stack.Push(j);
        best[j] = nextCount++;
        j = L[j] - 1;
      }

      if (mark[j])
      {
        // existing cycle
        var countToAdd = best[j];
        while (stack.Count > 0)
        {
          var k = stack.Pop();
          mark[k] = true;
          best[k] = ++countToAdd;
        }
      }
      else
      {
        //new cycle
        var newCycleLength = nextCount - best[j];
        while (true)
        {
          var k = stack.Pop();
          mark[k] = true;
          best[k] = newCycleLength;
          if (k == j) break;
        }
        while (stack.Count > 0)
        {
          var k = stack.Pop();
          mark[k] = true;
          best[k] = ++newCycleLength;
        }
      }
    }

    return best.Max();
  }

}