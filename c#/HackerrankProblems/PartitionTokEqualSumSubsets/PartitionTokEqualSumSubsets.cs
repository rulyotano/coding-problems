
public partial class Solution
{
    public bool CanPartitionKSubsets(int[] nums, int k)
    {
        nums = nums.OrderByDescending(n => n).ToArray();
        var average = Average(nums, k);
        if (average == -1) return false;
        var sets = new Set[k];
        for (var i = 0; i < k; i++)
        {
            sets[i] = new Set { AmountLeft = average };
        }

        var availableItems = new Dictionary<int, bool>();
        for (var i = 0; i < nums.Length; i++)
        {
            availableItems.Add(i, true);
        }

        return CanPartition(sets, availableItems, nums);
    }

    private bool CanPartition(Set[] sets, Dictionary<int, bool> availableItems, int[] nums)
    {
        if (sets.All(s => s.IsCompleted()) && availableItems.Count == 0) return true;
        foreach (var availableItem in availableItems.ToList())
        {
            var value = nums[availableItem.Key];
            foreach (var set in sets.Where(s => s.CanAdd(value)))
            {
                set.Add(value);
                availableItems.Remove(availableItem.Key);
                if (CanPartition(sets, availableItems, nums)) return true;
                availableItems.Add(availableItem.Key, true);
                set.Remove(value);
            }
        }

        return false;
    }

    private int Average(int[] nums, int k)
    {
        var sum = nums.Sum();
        if (sum % k != 0) return -1;
        return sum / k;
    }
}

public class Set
{
    public int AmountLeft { get; set; }

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
