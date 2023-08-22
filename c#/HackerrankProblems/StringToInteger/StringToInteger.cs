using System.Text;

public class StringToIntegerSolution
{

  public int MyAtoi(string s)
  {
    var parser = new Parser(s);
    var root = Root.Parse(parser);
    return root.ParseValue();
  }

  public class Parser
  {
    private readonly string _s;
    private int _currentIndex = -1;

    public Parser(string s)
    {
      _s = s.TrimStart();
    }

    public bool LookAhead(out char lookAhead)
    {
      lookAhead = default;
      if (IsFinished()) return false;
      lookAhead = _s[_currentIndex + 1];
      return true;
    }
    public bool IsFinished() => _currentIndex >= _s.Length - 1;
    public void MoveNext()
    {
      _currentIndex++;
    }
  }

  public class Root
  {
    public Sign Sign { get; set; }
    public Number Number { get; set; }
    public Rest Rest { get; set; }

    public static Root Parse(Parser parser)
    {
      var root = new Root();
      root.Sign = Sign.Parse(parser);
      root.Number = Number.Parse(parser);
      root.Rest = Rest.Parse(parser);
      return root;
    }

    public int ParseValue() => Number.ParseValue(Sign.Positive);
  }

  public class Sign
  {
    public bool Positive { get; private set; } = true;
    public static Sign Parse(Parser parser)
    {
      var result = new Sign();
      parser.LookAhead(out char lookAhead);
      if (lookAhead == '+' || lookAhead == '-')
      {
        parser.MoveNext();
        if (lookAhead == '-') result.Positive = false;
      }
      return result;
    }

  }

  public class Number
  {
    public string Value { get; private set; } = "0";

    public static Number Parse(Parser parser)
    {
      var result = new Number();
      var builder = new StringBuilder();

      while (parser.LookAhead(out char lookAhead) && '0' <= lookAhead && lookAhead <= '9')
      {
        builder.Append(lookAhead);
        parser.MoveNext();
      }

      if (builder.Length > 0)
      {
        result.Value = builder.ToString();
      }

      return result;
    }

    public int ParseValue(bool positive)
    {
      var trimmedValue = Value.TrimStart('0');
      if (trimmedValue == string.Empty) trimmedValue = "0";
      var maxString = positive ? int.MaxValue.ToString() : int.MinValue.ToString().Substring(1);
      if (trimmedValue.Length < maxString.Length || trimmedValue.Length == maxString.Length && trimmedValue.CompareTo(maxString) < 0) return int.Parse(trimmedValue) * (positive ? 1 : -1);
      return positive ? int.MaxValue : int.MinValue;
    }
  }

  public class Rest
  {
    public static Rest Parse(Parser parser) => new Rest();
  }

}
