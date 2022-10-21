class MeetingRoomsIISolution
{


  public int MinMeetingRooms(int[][] intervals)
  {
    PriorityQueue<bool, float> openAndCloseIntervals = new();

    foreach (var interval in intervals)
    {
      openAndCloseIntervals.Enqueue(true, interval[0]);
      openAndCloseIntervals.Enqueue(false, interval[1] - 0.5F);
    }

    var minimum = 0;
    var currentCount = 0;
    while (openAndCloseIntervals.Count > 0)
    {
      var isOpen = openAndCloseIntervals.Dequeue();
      currentCount += (isOpen ? 1 : -1);
      minimum = Math.Max(minimum, currentCount);
    };

    return minimum;
  }
};