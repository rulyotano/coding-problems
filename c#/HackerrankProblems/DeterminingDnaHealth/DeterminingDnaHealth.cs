using System.Text;

public class DeterminingDnaHealth
{
  public static void Main(string[] args)
  {
    int n = Convert.ToInt32(Console.ReadLine().Trim());

    Gene[] genes = Console.ReadLine().TrimEnd().Split(' ').Select((g, i) => new Gene { Value = g, Index = i }).ToArray();

    Console.ReadLine().TrimEnd().Split(' ').Select((healthTemp, i) => { genes[i].Health = Convert.ToInt32(healthTemp); return 1; }).ToList();

    int s = Convert.ToInt32(Console.ReadLine().Trim());

    var maxHealthSum = 0;
    var minHealthSum = int.MaxValue;
    for (int sItr = 0; sItr < s; sItr++)

    {
      string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

      int first = Convert.ToInt32(firstMultipleInput[0]);

      int last = Convert.ToInt32(firstMultipleInput[1]);

      string d = firstMultipleInput[2];

      var prefixIndexes = BuildPrefixMap(d, new[] { 1, 2 });
      var currentHealthSum = 0;

      foreach (var gene in genes.Skip(first).Take(last - first + 1))
      {
        var prefix = gene.GetPrefix();
        if (prefixIndexes.ContainsKey(prefix))
        {
          foreach (var possibleIndex in prefixIndexes[prefix])
          {
            if (!IsMatch(gene.Index, possibleIndex)) continue;
            currentHealthSum += gene.Health;
          }
        }
      }

      bool IsMatch(int geneIndex, int possibleIndex)
      {
        var gene = genes[geneIndex].Value;
        var leftLength = d.Length - possibleIndex;
        if (leftLength < gene.Length) return false;
        for (int i = 0; i < gene.Length; i++)
        {
          if (gene[i] != d[possibleIndex + i]) return false;
        }
        return true;
      }

      if (maxHealthSum < currentHealthSum)
      {
        maxHealthSum = currentHealthSum;
      }
      if (minHealthSum > currentHealthSum)
      {
        minHealthSum = currentHealthSum;
      }
    }
    Console.WriteLine($"{minHealthSum} {maxHealthSum}");
  }

  private static IDictionary<string, IList<int>> BuildPrefixMap(string dnaStrand, IEnumerable<int> prefixSizes)
  {
    var result = new Dictionary<string, IList<int>>();
    var builder = new StringBuilder();

    foreach (var prefixSize in prefixSizes)
    {
      for (int i = 0; i <= dnaStrand.Length - prefixSize; i++)
      {
        var currentChar = dnaStrand[i];
        builder.Append(currentChar);
        if (i >= prefixSize - 1)
        {
          var prefix = builder.ToString();
          var prefixIndex = i - prefixSize + 1;
          if (result.ContainsKey(prefix))
          {
            result[prefix].Add(prefixIndex);
          }
          else
          {
            result.Add(prefix, new List<int> { prefixIndex });
          }
          builder.Remove(0, 1);
        }
      }
    }
    return result;
  }

  class Gene
  {
    public int Index { get; set; }

    public string Value { get; set; }

    public int Health { get; set; }

    public string GetPrefix() => Value.Length > 1 ? Value.Substring(0, 2) : Value.Substring(0, 1);
  };
}