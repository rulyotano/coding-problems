using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerrankProblems.ConnectedCellsInGrid
{
    internal class ConnectedCellsInGrid
    {
        public static int connectedCell(List<List<int>> matrix)
        {
            var n = matrix.Count;
            var m = matrix[0].Count;
            var marks = new bool[n, m];
            var maxCount = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i][j] == 0 || marks[i, j]) continue;
                    var queue = new Queue<Cell>();
                    queue.Enqueue(new Cell { I = i, J = j });
                    marks[i, j] = true;
                    var currentCount = 0;
                    while (queue.Count > 0)
                    {
                        currentCount++;
                        var current = queue.Dequeue();
                        foreach (var currentChild in current.GetValidChilds(matrix, marks))
                        {
                            queue.Enqueue(currentChild);
                            marks[currentChild.I, currentChild.J] = true;
                        }
                    }
                    maxCount = Math.Max(maxCount, currentCount);
                }
            }

            return maxCount;
        }

        class Cell
        {
            public int I { get; set; }
            public int J { get; set; }
            public static readonly Direction[] _directions = new Direction[] { new Up(), new UpRight(), new Right(), new DownRight(), new Down(), new DownLeft(), new Left(), new UpLeft() };
            public bool IsInside(bool[,] marks) => I >= 0 && J >= 0 && I < marks.GetLength(0) && J < marks.GetLength(1);
            public bool IsNotMarked(bool[,] marks) => !marks[I, J];
            public bool IsNotEmpty(List<List<int>> matrix) => matrix[I][J] == 1;
            public IEnumerable<Cell> GetValidChilds(List<List<int>> matrix, bool[,] marks)
                => _directions.Select(direction => direction.Move(this))
                              .Where(child => child.IsInside(marks) && child.IsNotEmpty(matrix) && child.IsNotMarked(marks));
        }

        abstract class Direction
        {
            public abstract Cell Move(Cell cell);
        }

        class Up : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I - 1, J = cell.J };
        }
        class UpRight : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I - 1, J = cell.J + 1 };
        }
        class Right : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I, J = cell.J + 1 };
        }
        class DownRight : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I + 1, J = cell.J + 1 };
        }
        class Down : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I + 1, J = cell.J };
        }
        class DownLeft : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I + 1, J = cell.J - 1 };
        }
        class Left : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I, J = cell.J - 1 };
        }
        class UpLeft : Direction
        {
            public override Cell Move(Cell cell) => new Cell { I = cell.I - 1, J = cell.J - 1 };
        }
    }
}
