partial class MultiplesOf3And5
{
  static void Main(String[] args)
  {
    int t = Convert.ToInt32(Console.ReadLine());
    for (int a0 = 0; a0 < t; a0++)
    {
      int n = Convert.ToInt32(Console.ReadLine());
      Solve(n);
    }
  }

  static void Solve(int n)
  {
    var closet3 = FindCloset(3);
    var closet5 = FindCloset(5);
    var closet15 = FindCloset(15);
    var n3 = closet3 / 3L;
    var n5 = closet5 / 5L;
    var n15 = closet15 / 15L;

    int FindCloset(int k)
    {
      var current = n - 1;      
      if (current < k) return 0;
      while (current % k != 0)
      {
        current--;
      }
      return current;
    }

    Console.WriteLine(3L*(n3*(n3 + 1))/2 + 5L*(n5*(n5 + 1))/2 - 15L*(n15*(n15 + 1))/2);
  }

  static void SolveTimeOut(int n)
  {
    var result = 0;
    var mult3 = 3;
    var mult5 = 5;
    while (mult3 < n || mult5 < n)
    {
      var min = TakeMinAndUpdate();
      result += min;
    }

    int TakeMinAndUpdate()
    {
      if (mult3 < mult5)
      {
        var t3 = mult3;
        mult3 += 3;
        return t3;
      }
      if (mult5 < mult3)
      {
        var t5 = mult5;
        mult5 += 5;
        return t5;
      }
      var tBoth = mult3;
      mult3 += 3;
      mult5 += 5;
      return tBoth;
    }

    Console.WriteLine(result);
  }
}