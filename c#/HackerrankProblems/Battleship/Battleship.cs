class BattleshipSolution {
  
  public double getHitProbability(int R, int C, int[,] G) {
    var count = 0D;
    for (int i = 0; i < R; i++)
    {
      for (int j = 0; j < C; j++)
      {
        if (G[i, j] == 1) count++;
      }
    }
    return count / (R*C);
  }
  
}
