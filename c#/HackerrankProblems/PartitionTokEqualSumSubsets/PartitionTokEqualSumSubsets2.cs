
using System.Collections;

public class SolutionPartitionKSubset2
{
    public bool CanPartitionKSubsets1(int[] nums, int k)
    {
        nums = nums.OrderByDescending(n => n).ToArray();
        var average = Average(nums, k);
        if (average == -1) return false;

        var pickedItems = 0;
        var cache = new Dictionary<int, bool>();

        bool CanPartition(Set current)
        {
            if (cache.ContainsKey(pickedItems)) return cache[pickedItems];
            if (current.Index == k) return true;

            bool ReturningWithCache(bool result)
            {
                cache.TryAdd(pickedItems, result);
                return result;
            }

            if (current.IsCompleted())
            {
                return ReturningWithCache(CanPartition(new Set { Index = current.Index + 1, AmountLeft = average }));
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if ((pickedItems & (1 << i)) == (1 << i)) continue;
                if (!current.CanAdd(nums[i])) return ReturningWithCache(false);
                current.Add(nums[i]);
                pickedItems |= (1 << i);
                if (CanPartition(current)) return ReturningWithCache(true);
                current.Remove(nums[i]);
                pickedItems ^= (1 << i);
            }

            return ReturningWithCache(false);
        }

        return CanPartition(new Set { AmountLeft = average, Index = 1 });
    }



    private int Average(int[] nums, int k)
    {
        var sum = nums.Sum();
        if (sum % k != 0) return -1;
        return sum / k;
    }

    public class Set
    {
        public int AmountLeft { get; set; }

        public int Index { get; set; }

        public bool IsCompleted() => AmountLeft == 0;

        public bool CanAdd(int item) => AmountLeft - item >= 0;

        public void Add(int item)
        {
            AmountLeft -= item;
        }

        public void Remove(int item)
        {
            AmountLeft += item;
        }
    }

}