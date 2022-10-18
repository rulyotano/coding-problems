using System.Collections;

public partial class Solution
{
    public int MaxLength(IList<string> arr)
    {
        var mark = new Mark();
        arr = arr.Where(it => !HasRepeatedChars(it)).ToList();

        return Solve(mark, arr, 0, arr.Count - 1);
    }

    private static bool HasRepeatedChars(string s) => s != string.Join("", s.Distinct());

    private int Solve(Mark mark, IList<string> arr, int i, int j)
    {
        if (i > j || mark.IsEmpty()) return 0;
        if (!mark.Can(arr[i])) return Solve(mark, arr, i + 1, j);
        if (i == j) return arr[i].Length;
        mark.Without(arr[i]);
        var variant1 = Solve(mark, arr, i + 1, j) + arr[i].Length;
        mark.With(arr[i]);
        var variant2 = Solve(mark, arr, i + 1, j);
        return Math.Max(variant1, variant2);
    }

    class Mark
    {
        private const string Abc = "abcdefghijklmnopqrstuvwxyz";
        private readonly BitArray _array;
        public Mark()
        {
            _array = new BitArray(Abc.Length);
        }

        public void Without(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                _array[Abc.IndexOf(s[i])] = true;
            }
        }

        public void With(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                _array[Abc.IndexOf(s[i])] = false;
            }
        }

        public bool Can(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (_array[Abc.IndexOf(s[i])]) return false;
            }
            return true;
        }

        public bool IsEmpty() => _array.Cast<bool>().All(it => it);

        public override int GetHashCode()
        {
            return _array.GetHashCode();
        }
    }
}