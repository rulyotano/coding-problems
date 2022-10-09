using System.Collections;

class LargestPrimeFactor
{
  const int MaxPrimeDivisor = 1000000;

  static List<long> _primes = new List<long>();
  public static void Main(String[] args)
  {
    Criba();
    int t = Convert.ToInt32(Console.ReadLine());
    for (int a0 = 0; a0 < t; a0++)
    {
      long n = Convert.ToInt64(Console.ReadLine());
      Solve(10);
    }
  }

  static void Solve(long n)
  {
    var currentPrimeIndex = 0;
    while (n > 1 && currentPrimeIndex < _primes.Count)
    {
      if (n % _primes[currentPrimeIndex] == 0)
      {
        n /= _primes[currentPrimeIndex];
      }
      else 
      {
        currentPrimeIndex++;
      }
    }
    if (n > 1)
    {
      Console.WriteLine(n);
    }
    else 
    {
      Console.WriteLine(_primes[currentPrimeIndex]);
    }
  }

  static void Criba()
  {
    var bitArray = new BitArray(MaxPrimeDivisor);
    var primeNumber = 2;
    while (primeNumber < MaxPrimeDivisor)
    {
      _primes.Add(primeNumber);
      for (int i = primeNumber; i < MaxPrimeDivisor; i+=primeNumber)
      {
        bitArray.Set(i, true);
      }
      while (primeNumber < MaxPrimeDivisor && bitArray[primeNumber])
      {
        primeNumber++;
      }
    }
  }
}