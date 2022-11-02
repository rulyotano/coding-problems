class PortalsSolution
{
  private const char Start = 'S';
  private const char End = 'E';
  private const char Wall = '#';

  public int getSecondsRequired(int R, int C, char[,] G)
  {
    var pMap = new Dictionary<char, List<(int i, int j)>>();
    var marks = new bool[R, C];
    Point start = null;
    var queue = new Queue<Point>();

    for (int i = 0; i < R; i++)
    {
      for (int j = 0; j < C; j++)
      {
        var current = G[i, j];
        if (current == Start)
        {
          start = new Point { I = i, J = j };
          continue;
        }

        if (current == Wall)
        {
          marks[i, j] = true;
          continue;
        }

        if (Point.IsPortal(i, j, G))
        {
          if (pMap.ContainsKey(current))
            pMap[current].Add((i, j));
          else
            pMap.Add(current, new() { (i, j) });
        }
      }
    }

    queue.Enqueue(start);

    while (queue.Count > 0)
    {
      var current = queue.Dequeue();
      if (G[current.I, current.J] == End) return current.Count;
      foreach (var child in current.GetChildren(G, marks, pMap))
      {
        marks[child.I, child.J] = true;
        child.Count = current.Count + 1;
        queue.Enqueue(child);
      }
    }

    return -1;
  }

  class Point
  {
    private static readonly Movement[] _movements = new Movement[] { new Up(), new Down(), new Left(), new Right() };
    public int Count { get; set; }
    public int I { get; set; }
    public int J { get; set; }
    public bool IsInside(char[,] G) => I >= 0 && I < G.GetLength(0) && J >= 0 && J < G.GetLength(1);
    public bool IsNotMarked(bool[,] marks) => !marks[I, J];
    public static bool IsPortal(int i, int j, char[,] G) => 'a' <= G[i, j] && G[i, j] <= 'z';
    public bool IsPortal(char[,] G) => IsPortal(I, J, G);
    public IEnumerable<Point> GetChildren(char[,] G, bool[,] marks, IDictionary<char, List<(int I, int J)>> pMarks)
    {
      foreach (var nextPoint in _movements
                                  .Select(m => m.Move(I, J))
                                  .Where(point => point.IsInside(G) && point.IsNotMarked(marks)))
      {
        yield return nextPoint;
      }

      if (IsPortal(G))
      {
        foreach (var portal in pMarks[G[I, J]].Where(p => !marks[p.I, p.J]))
        {
          yield return new Point { I = portal.I, J = portal.J };
        }
      }
    }
  }

  abstract class Movement
  {
    public abstract Point Move(int i, int j);
  }

  class Up : Movement
  {
    public override Point Move(int i, int j) => new Point { I = i - 1, J = j };
  }

  class Down : Movement
  {
    public override Point Move(int i, int j) => new Point { I = i + 1, J = j };
  }

  class Left : Movement
  {
    public override Point Move(int i, int j) => new Point { I = i, J = j - 1 };
  }

  class Right : Movement
  {
    public override Point Move(int i, int j) => new Point { I = i, J = j + 1 };
  }
}
