using System.Collections;

namespace prime.subtraction.operation;

// src: leetcode

public class Solution {
    private const int MaxNumber = 1000;
    public bool PrimeSubOperation(int[] nums) {
        var primes = FindPrimes();
        var previous = 0;

        for (var i = 0; i < nums.Length; i++)
        {
            var maxPrime = FindGreaterPrime(primes, nums[i], previous);
            if (nums[i] - maxPrime <= previous) return false;
            previous = nums[i] - maxPrime;
        }

        return true;
    }

    private static IList<int> FindPrimes()
    {
        var marks = new BitArray(MaxNumber);
        for (var i = 0; i < marks.Count; i++)
        {
            if (marks[i]) continue;

            var increase = i + 2;
            var j = i + increase;
            while (j < marks.Count)
            {
                marks[j] = true;
                j+=increase;
            }
        }

        var result = new List<int>();
        for (var i = 0; i < marks.Count; i++)
        {
            if (!marks[i])
            {
                result.Add(i+2);
            }
        }

        return result;
    } 

    private static int FindGreaterPrime(IEnumerable<int> primes, int number, int previous)
    {
        var found = 0;

        foreach (var prime in primes.Where(p => p < number))
        {
            if (number - prime > previous) found = prime;
        }

        return found;
    }
}