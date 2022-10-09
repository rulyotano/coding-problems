public class LargestPalindromeProduct {
    static List<int> _palindromes = new List<int>();

    public static void Main(String[] args) {
        GeneratePalindromes();
        int t = Convert.ToInt32(Console.ReadLine());
        for(int a0 = 0; a0 < t; a0++){
            int n = Convert.ToInt32(Console.ReadLine());
            Solve(n);
        }
    }

    static void Solve(int n)
    {
      int i = 1;
      for (; i < _palindromes.Count; i++)
      {
        if (_palindromes[i] >= n) break;
      }
      Console.WriteLine(_palindromes[i - 1]);
    }

    static void GeneratePalindromes()
    {
      for (int i = 100; i < 1000; i++)
      {
        for (int j = i; j < 1000; j++)
        {
          var num = i*j;
          if (IsPalindrome(num))
          {
            _palindromes.Add(num);
          }
        }
      }
      _palindromes.Sort();
    }

    static bool IsPalindrome(int k)
    {
      return k/100000 == k%10 && ((k/10000)%10 == (k/10)%10) && ((k/1000)%10 == (k/100)%10);
    }
}