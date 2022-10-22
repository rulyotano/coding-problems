class GridlandMetroResult
{
  public static long gridlandMetro(long n, long m, int k, List<List<int>> tracks)
  {
    var groupedByRow = tracks.GroupBy(track => track[0]);
    return n * m - groupedByRow.Select(track => CountInRow(track)).Sum();
  }


  private static long CountInRow(IEnumerable<List<int>> tracks)
  {
    var trackArray = tracks.ToArray();
    var starts = new List<TrackPart>();
    var endings = new List<TrackPart>();
    for (int i = 0; i < trackArray.Length; i++)
    {
      starts.Add(new TrackPart { Key = i, Value = trackArray[i][1] });
      endings.Add(new TrackPart { Key = i, Value = trackArray[i][2] });
    }
    starts = starts.OrderBy(s => s.Value).ToList();
    endings = endings.OrderBy(s => s.Value).ToList();
    var startIndex = 0;
    var endIndex = 0;
    var rowSum = 0L;
    TrackPart currentStart = null;
    var startSets = new HashSet<int>();
    while (startIndex < starts.Count)
    {
      if (currentStart is null)
      {
        currentStart = starts[startIndex];
        startSets.Add(starts[startIndex].Key);
        startIndex++;
        continue;
      }

      if (starts[startIndex].Value <= endings[endIndex].Value)
      {
        startSets.Add(starts[startIndex].Key);
        startIndex++;
        continue;
      }

      startSets.Remove(endings[endIndex].Key);
      if (startSets.Count == 0)
      {
        rowSum += (long)(endings[endIndex].Value - currentStart.Value + 1);
        currentStart = null;
      }
      endIndex++;
    }

    if (currentStart is not null)
    {
      rowSum += (long) (endings[endings.Count - 1].Value - currentStart.Value + 1);
    }

    return rowSum;
  }

  class TrackPart
  {
    public int Key { get; set; }
    public int Value { get; set; }
  }
}