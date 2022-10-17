public partial class Solution
{
  public void Rotate(int[][] matrix)
  {
    var n = matrix.Length;

    for (int ring = 0; ring <= (n - 1) / 2; ring++)
    {
      for (int col = ring; col < n - 1 - ring; col++)
      {
        var i = ring;
        var j = col;
        var value = matrix[i][j];
        do
        {
          var (nexti, nextj) = FindNext(i, j, n);
          var t = matrix[nexti][nextj];
          matrix[nexti][nextj] = value;
          value = t;
          i = nexti;
          j = nextj;
        } while (i != ring || j != col);
      }
    }
  }

  public (int nexti, int nextj) FindNext(int i, int j, int n) => (j, n - (i + 1));
}