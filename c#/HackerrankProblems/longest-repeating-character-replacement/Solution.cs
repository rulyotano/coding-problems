namespace longest.repeating.character.replacement;

// src: leetcode

public class Solution {
    public int CharacterReplacement(string s, int k) 
    {
        var existingChars = new HashSet<char>();
        foreach (var currentChar in s)
        {
            existingChars.Add(currentChar);
        }
        var best = 0;
        foreach (var currentChar in existingChars)
        {
            best = Math.Max(best, CharacterReplacement(currentChar, s, k));
        }
        return best;
    }

    private int CharacterReplacement(char c, string s, int k)
    {
        var startIndex = 0;
        var endIndex = 0;
        var currentK = k;
        var best = 0;

        while (endIndex < s.Length)
        {
            while (endIndex < s.Length && (currentK > 0 || s[endIndex] == c))
            {
                if (s[endIndex] != c) currentK--;
                endIndex++;
            }
            best = Math.Max(best, endIndex - startIndex);
            if (s[startIndex] != c) currentK++;
            startIndex++;
            if (k == 0)
            {
                currentK = 0;
                endIndex = Math.Max(endIndex, startIndex);
            }
        }

        return best;
    }
}