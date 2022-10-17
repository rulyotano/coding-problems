public partial class Solution {
    public bool CheckPalindromeFormation(string a, string b)
    {
        var i = MaxPrefixMatchingSuffix(a, b);
        if (IsPalindrome(a, i + 1, a.Length - 1 - (i + 1)) ||
            IsPalindrome(b, i + 1, b.Length - 1 - (i + 1))) return true;
        i = MaxPrefixMatchingSuffix(b, a);
        return IsPalindrome(a, i + 1, a.Length - 1 - (i + 1)) ||
               IsPalindrome(b, i + 1, b.Length - 1 - (i + 1));
    }

    private int MaxPrefixMatchingSuffix(string a, string b)
    {
        var i = -1;

        while (i < a.Length - 1 && a[i + 1] == b[b.Length - 1 - (i + 1)])
        {
            i++;
        }
        return i;
    }

    private bool IsPalindrome(string s, int i, int j)
    {
        while (i < j)
        {
            if (s[i] != s[j]) return false;
            i++;
            j--;
        }

        return true;
    }
}