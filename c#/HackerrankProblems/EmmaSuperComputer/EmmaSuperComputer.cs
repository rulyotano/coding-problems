partial class Result
{

  /*
   * Complete the 'twoPluses' function below.
   *
   * The function is expected to return an INTEGER.
   * The function accepts STRING_ARRAY grid as parameter.
   */

  public static int twoPluses(List<string> grid)
  {
    var mat = GetMatrix(grid);
    var (b1, i1, j1) = FindBiggest(mat);
    DeleteBiggest(mat, i1, j1);
    var (b2, _, _) = FindBiggest(mat);

    return b1 * b2;
  }

  private static bool[,] GetMatrix(List<string> grid)
  {
    var mat = new bool[grid.Count, grid[0].Length];

    for (int i = 0; i < grid.Count; i++)
    {
      for (int j = 0; j < grid[i].Length; j++)
      {
        if (grid[i][j] == 'G') mat[i, j] = true;
      }
    }

    return mat;
  }

  private static (int biggestCount, int i, int j) FindBiggest(bool[,] mat)
  {
    var biggest = 0;
    var bi = -1;
    var bj = -1;
    for (int i = 0; i < mat.GetLength(0); i++)
    {
      for (int j = 0; j < mat.GetLength(1); j++)
      {
        if (!BiggerFit(biggest, i, j, mat)) continue;
        var current = CountCross(mat, i, j);
        if (current > biggest)
        {
          biggest = current;
          bi = i;
          bj = j;
        }
      }
    }
    return (biggest, bi, bj);
  }

  private static bool BiggerFit(int biggest, int i, int j, bool[,] mat)
  {
    var horizontal = Math.Min(i, mat.GetLength(0) - i - 1);
    var vertical = Math.Min(j, mat.GetLength(1) - j - 1);
    return (Math.Min(horizontal, vertical) + 1) >  (biggest > 0 ? (biggest / 2) + 1 : 0);
  }

  private static int CountCross(bool[,] mat, int i, int j)
  {
    return GetCross(mat, i, j).Count();
  }

  private static void DeleteBiggest(bool[,] mat, int bi, int bj)
  {
    foreach (var (i, j) in GetCross(mat, bi, bj).ToArray())
    {
      mat[i, j] = false;
    }
  }

  private static IEnumerable<(int i, int j)> GetCross(bool[,] mat, int i, int j)
  {
    if (!mat[i, j]) yield break;
    yield return (i, j);
    var directions = new Direction[] { new Up { I = i, J = j }, new Down { I = i, J = j }, new Left { I = i, J = j }, new Right { I = i, J = j } };
    foreach (var direction in directions)
    {
      direction.Move();
    }
    while (directions.All(it => it.Fit(mat) && mat[it.I, it.J]))
    {
      foreach (var direction in directions)
      {
        yield return (direction.I, direction.J);
        direction.Move();
      }
    }
  }

  abstract class Direction
  {
    public int I { get; set; }
    public int J { get; set; }

    public bool Fit(bool[,] mat) => I >= 0 && J >= 0 && I < mat.GetLength(0) && J < mat.GetLength(1);
    public abstract void Move();
  }
  class Up : Direction
  {
    public override void Move()
    {
      I--;
    }
  }
  class Down : Direction
  {
    public override void Move()
    {
      I++;
    }
  }

  class Left : Direction
  {
    public override void Move()
    {
      J--;
    }
  }
  class Right : Direction
  {
    public override void Move()
    {
      J++;
    }
  }
}