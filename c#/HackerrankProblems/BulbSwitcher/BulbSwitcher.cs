using System.Collections;

public class BulbSwitcherSolution
{
  // 33/35 cases
  public int BulbSwitch1(int n)
  {
    if (n == 0) return 0;
    var count = new BitArray(n);
    for (int i = 2; i <= n; i++)
    {
      var iK = i - 1;
      while (iK < n)
      {
        count[iK] = !count[iK];
        iK += i;
      }
    }
    return n - count.Cast<bool>().Count(it => it);
  }

  // removing last loop
  public int BulbSwitch2(int n)
  {
    if (n == 0) return 0;
    var count = new BitArray(n);
    var numberCount = 1;
    for (int i = 2; i <= n; i++)
    {
      var iK = i - 1;
      while (iK < n)
      {
        count[iK] = !count[iK];
        iK += i;
      }
      if (!count[i - 1]) numberCount++;
    }
    return numberCount;
  }

  // prime numbers
  public int BulbSwitchPrimes(int n)
  {
    var primesLowerThanN = Criba(n);
    var bits = new BitArray(n);
    var count = 1;

    for (int i = 1; i < n; i++)
    {
      bits[i] = !bits[i];
      var number = i + 1;
      var sqrt = Math.Ceiling(Math.Sqrt(number));
      for (int primeIndex = 0; primesLowerThanN[primeIndex] <= sqrt; primeIndex++)
      {
        var prime = primesLowerThanN[primeIndex];
        if (number % prime == 0)
        {
          bits[i] = bits[i] ^ (number/prime <= 1 || !bits[number/prime - 1]);
        }
      }
      if (!bits[i]) count ++;
    }

    return count;
  }

  public int[] Criba(int n)
  {
    var max = (int) Math.Ceiling(Math.Sqrt(n));
    var criba = new BitArray(n + 1);
    for (int i = 2; i <= max; i++)
    {
      if (criba[i]) continue;
      var j = i * 2;
      while (j <= n)
      {
        criba[j] = true;
        j+=i;
      }
    }
    var result = new List<int>();
    for (int i = 2; i < criba.Count; i++)
    {
      if (criba[i]) continue;
      result.Add(i);
    }
    return result.ToArray();
  }
}