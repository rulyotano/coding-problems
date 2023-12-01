namespace NumberOfBurgersWithNoWaste;

// src leetcode

public class Solution {
    public IList<int> NumOfBurgers(int tomatoSlices, int cheeseSlices) {
        var emptyResult = new List<int>();
        if (tomatoSlices % 2 != 0) return emptyResult;
        var prepareAllSmall = tomatoSlices / 2;
        if (prepareAllSmall < cheeseSlices) return emptyResult;
        if (prepareAllSmall == cheeseSlices) return new List<int> { 0, prepareAllSmall };
        var smallExceed = prepareAllSmall - cheeseSlices;
        if (smallExceed > cheeseSlices) return emptyResult;

        return new List<int> { smallExceed, prepareAllSmall - 2 * smallExceed };
    }
}