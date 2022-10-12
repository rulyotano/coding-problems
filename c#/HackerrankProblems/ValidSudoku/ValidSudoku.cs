using System.Collections;
using System.Collections.Generic;

public partial class Solution
{
  public bool IsValidSudoku(char[][] board)
  {
    for (int i = 0; i < 9; i++)
    {
      if (!CheckColumn(board, i)) return false;
      if (!CheckRow(board, i)) return false;      
      if (!CheckSquare(board, (i/3)*3, (i%3)*3)) return false;
    }
    return true;
  }

  private bool CheckRow(char[][] board, int rowIndex)
  {
    var cellsToCheck = new List<char>();
    for (int i = 0; i < board[rowIndex].Length; i++)
    {
      cellsToCheck.Add(board[rowIndex][i]);
    }
    return IsValid(cellsToCheck);
  }

  private bool CheckColumn(char[][] board, int colIndex)
  {
    var cellsToCheck = new List<char>();
    for (int i = 0; i < board.Length; i++)
    {
      cellsToCheck.Add(board[i][colIndex]);
    }
    return IsValid(cellsToCheck);
  }

  private bool CheckSquare(char[][] board, int rowIndex, int colIndex)
  {
    var cellsToCheck = new List<char>();
    for (int i = rowIndex; i < rowIndex + 3; i++)
    {
      for (int j = colIndex; j < colIndex + 3; j++)
      {
        cellsToCheck.Add(board[i][j]);
      }
    }
    return IsValid(cellsToCheck);
  }

  private bool IsValid(List<char> cells)
  {
    if (cells.Count != 9) return false;
    var checks = new Dictionary<char, bool>();
    foreach (var cell in cells)
    {
      if (cell == '.') continue;
      if (checks.ContainsKey(cell)) return false;
      checks.Add(cell, true);
    }
    return true;
  }
}