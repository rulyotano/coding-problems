using System.Text;

class RegularExpressionMatchingSolution
{
  private const char Wildcard = '.';
  private const char MatchMany = '*';
  public bool IsMatch(string s, string p)
  {
    var pattern = new Pattern(p);
    return pattern.IsMatch(s);
  }

  class Pattern
  {
    private List<PatternItem> _items;
    public Pattern(string p)
    {
      _items = Parse(p);
    }

    public bool IsMatch(string s)
    {
      return LocalIsMatch(0, 0);

      bool LocalIsMatch(int i, int iPattern)
      {
        if (iPattern == _items.Count && i == s.Length) return true;
        if (iPattern >= _items.Count) return false;
        var pattern = _items[iPattern];

        foreach (var nextIndex in pattern.GetNextIndexes(s, i).Reverse())
        {
          if (LocalIsMatch(nextIndex, iPattern + 1)) return true;
        }
        return false;
      }
    }

    private static List<PatternItem> Parse(string p)
    {
      var currentBuilder = new StringBuilder();
      var result = new List<PatternItem>();
      for (int i = 0; i < p.Length; i++)
      {
        char current = p[i];
        if (current == MatchMany)
        {
          var previous = currentBuilder[currentBuilder.Length - 1];
          currentBuilder.Remove(currentBuilder.Length - 1, 1);
          AddFixedItemIfSome();
          result.Add(new ZeroOrMore { Character = previous });
        }
        else
        {
          currentBuilder.Append(current);
        }
      }

      AddFixedItemIfSome();
      CollapseWildcards();
      return result;

      void AddFixedItemIfSome()
      {
        if (currentBuilder.Length > 0)
        {
          result.Add(new Fixed { Pattern = currentBuilder.ToString() });
          currentBuilder.Clear();
        }
      }
      void CollapseWildcards()
      {
        var newResult = new List<PatternItem>();
        var currentZeroMoreGroup = new List<ZeroOrMore>();
        var hasWildcard = false;
        for (int i = 0; i < result!.Count; i++)
        {
          if (result[i] is Fixed)
          {
            AddZeroOrMoreIfSome();
            newResult.Add(result[i]);
          }
          else
          {
            var zeroOrMore = result[i] as ZeroOrMore;
            if (zeroOrMore!.Character == Wildcard) hasWildcard = true;
            currentZeroMoreGroup.Add(zeroOrMore);
          }
        }
        AddZeroOrMoreIfSome();
        result = newResult;

        void AddZeroOrMoreIfSome()
        {
          if (currentZeroMoreGroup.Count > 0)
          {
            if (hasWildcard)
            {
              newResult.Add(new ZeroOrMore { Character = Wildcard });
            }
            else
            {
              foreach (var zeroOrMore in currentZeroMoreGroup)
              {
                newResult.Add(zeroOrMore);
              }
            }
            currentZeroMoreGroup.Clear();
            hasWildcard = false;
          }
        }
      }
    }

  }

  abstract class PatternItem
  {
    public abstract IEnumerable<int> GetNextIndexes(string s, int i);
  }

  class ZeroOrMore : PatternItem
  {
    public char Character { get; set; }
    public override IEnumerable<int> GetNextIndexes(string s, int i)
    {
      var result = new List<int> { i };
      var current = i;
      while (current < s.Length && (Character == Wildcard || s[current] == Character)) result.Add(++current);
      return result;
    }
  }
  class Fixed : PatternItem
  {
    public string Pattern { get; set; }
    public override IEnumerable<int> GetNextIndexes(string s, int i)
    {
      for (int j = 0; j < Pattern.Length; j++)
      {
        var sj = i + j;
        if (sj >= s.Length || Pattern[j] != Wildcard && Pattern[j] != s[sj]) return new List<int>();
      }
      return new List<int> { i + Pattern.Length };
    }
  }

}