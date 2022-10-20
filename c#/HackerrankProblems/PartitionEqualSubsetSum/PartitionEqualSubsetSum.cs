using System.Collections;
using System.Text;

public class PartitionEqualSubsetSumSolution
{
    public bool CanPartition(int[] nums)
    {
        Array.Sort(nums);
        var wanted = FindAverage(nums);
        if (wanted == -1) return false;
        var hash = new Dictionary<ulong, bool>();

        ulong GetKey(uint index, uint value)
        {
            ulong key = index;
            key <<= 32;
            key += value;
            return key;
        }

        bool IsPossiblePartition(int currentIndex, int currentValue)
        {
            var currentKey = GetKey((uint)currentIndex, (uint)currentValue);
            if (hash.ContainsKey(currentKey)) return hash[currentKey];
            if (currentValue < 0 || currentIndex == nums.Length) return false;
            if (currentValue == 0) return true;

            bool ReturnWithCache(bool localResult)
            {
                hash.TryAdd(currentKey, localResult);
                return localResult;
            }

            return ReturnWithCache(IsPossiblePartition(currentIndex + 1, currentValue - nums[currentIndex]) || IsPossiblePartition(currentIndex + 1, currentValue));
        }

        return IsPossiblePartition(0, wanted);
    }

    private int FindAverage(int[] nums)
    {
        var sum = nums.Sum();
        if (sum % 2 != 0) return -1;
        return sum / 2;
    }
}