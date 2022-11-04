class RabbitHole1_BadUnderstoodSolution
{

  public int getMaxVisitableWebpages(int N, int[] L)
  {
    var t = new SortedSet<int>();
    var maxCount = 0;
    var groupCounts = new Dictionary<int, int>();
    var marks = new int[L.Length];
    var nextGroup = 1;
    var queue = new Queue<int>();

    for (var i = 0; i < N; i++)
    {
      if (marks[i] > 0) continue;
      var currentGroup = nextGroup++;
      queue.Enqueue(i);
      marks[i] = currentGroup;
      var j = L[i] - 1;

      while (marks[j] == 0)
      {
        queue.Enqueue(j);
        marks[j] = currentGroup;
        j = L[j] - 1;
      }

      if (marks[j] == currentGroup)
      {
        var newCounts = queue.Count;
        groupCounts.Add(currentGroup, newCounts);
        maxCount = Math.Max(maxCount, newCounts);
        queue.Clear();
        continue;
      }

      var existingGroup = marks[j];
      var updatedCount = groupCounts[existingGroup] + queue.Count;
      groupCounts[existingGroup] = updatedCount;
      maxCount = Math.Max(maxCount, updatedCount);
      while (queue.Count > 0)
      {
        var indexToUpdate = queue.Dequeue();
        marks[indexToUpdate] = existingGroup;
      }
    }

    return maxCount;
  }

}