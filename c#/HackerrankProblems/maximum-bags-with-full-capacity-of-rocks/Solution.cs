namespace maximum.bags.with.full.capacity.of.rocks;
// src: leetcode

public class Solution {
    public int MaximumBags(int[] capacity, int[] rocks, int additionalRocks) {
        var differences = new int[capacity.Length];
        for (var i = 0; i < capacity.Length; i++)
        {
            differences[i] = capacity[i] - rocks[i];
        }

        Array.Sort(differences);
        var count = 0;
        var left = additionalRocks;
        var j = 0;
        while (j < differences.Length && differences[j] == 0) {count++; j++;}
        while (j < differences.Length && left > 0 && differences[j] <= left)
        {
            left -= differences[j];
            j++;
            count++;
        }
        return count;
    }
}
