using System.Collections;

namespace find.score.of.an.array.after.marking.all.elements;

// src: leetcode

public class Solution {
    public long FindScore(int[] nums) {
        var marks = new BitArray(nums.Length);
        var indexByNumber = nums.Select((number, index) => new NumberIndex(number, index)).OrderBy(it => it.Number).ToArray();

        long score = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            var item = indexByNumber[i];
            if (marks[item.Index]) continue;
            score+=item.Number;
            marks[item.Index] = true;
            if (item.Index > 0) marks[item.Index - 1] = true;
            if (item.Index < nums.Length - 1) marks[item.Index + 1] = true;
        }

        return score;
    }

    private record NumberIndex(int Number, int Index);
}