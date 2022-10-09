public class EvenFibonacciSum
{
  const long MaxNumber = 40000000000000000;
  static List<long> _evenFibonacci = new List<long>();
  static List<long> _evenFibonacciSum = new List<long>();

  public static void Main(String[] args)
  {
    CalculateCache();
    int t = Convert.ToInt32(Console.ReadLine());
    for (int a0 = 0; a0 < t; a0++)
    {
      long n = Convert.ToInt64(Console.ReadLine());
      Solve(n);
    }
  }

  static void CalculateCache()
  {
    foreach (var fibItem in Fibonacci(MaxNumber))
    {
      if (fibItem % 2 == 0)
      {
        _evenFibonacci.Add(fibItem);
        _evenFibonacciSum.Add(_evenFibonacciSum.Count == 0 ? fibItem : _evenFibonacciSum[_evenFibonacciSum.Count - 1] + fibItem);
      }
    }
  }

  static void Solve(long n)
  {
    var sumIndex = BestIndexBinarySearch(n);
    if (_evenFibonacci[sumIndex] > n) sumIndex--;
    if (sumIndex < 0)
    {
      Console.WriteLine(0);
      return;
    }
    Console.WriteLine(_evenFibonacciSum[sumIndex]);
  }

  static int BestIndexBinarySearch(long number)
  {
    int l = 0;
    int r = _evenFibonacci.Count - 1;
    int middle = -1;
    while (l <= r)
    {
      middle = (l + r) / 2;
      if (_evenFibonacci[middle] < number)
      {
        l = middle + 1;
      }
      else if (_evenFibonacci[middle] > number)
      {
        r = middle - 1;
      }
      else
      {
        break;
      }
    }
    return middle;
  }

  static IEnumerable<long> Fibonacci(long n)
  {
    long previous = 0;
    long current = 1;
    long next = previous + current;
    while (current <= n)
    {
      yield return current;
      previous = current;
      current = next;
      next = previous + current;
    }
  }
}