using System.Collections;
using System.Text;

public class PartitionEqualSubsetSumSolution2
{
    public bool CanPartition(int[] nums)
    {
        Array.Sort(nums);
        var wanted = FindAverage(nums);
        if (wanted == -1) return false;
        var currentSum = 0;
        var currentSelected = new CurrentSelection(nums.Length);
        var hash = new Dictionary<string, bool>();

        bool IsPossiblePartition(int previousIndex = -1)
        {
            var currentKey = currentSelected.GetKey(nums);
            if (hash.ContainsKey(currentKey)) return hash[currentKey];
            if (currentSum == wanted) return true;

            bool ReturnWithCache(bool localResult)
            {
                hash.TryAdd(currentKey, localResult);
                return localResult;
            }

            for (int i = previousIndex + 1; i < nums.Length; i++)
            {
                if (currentSelected.IsSelected(i)) continue;
                if (currentSum + nums[i] > wanted) return ReturnWithCache(false);
                currentSelected.Select(i);
                currentSum += nums[i];
                var result = IsPossiblePartition(i);
                if (result) return ReturnWithCache(result);
                currentSelected.Unselect(i);
                currentSum -= nums[i];
            }

            return ReturnWithCache(false);
        }

        return IsPossiblePartition();
    }

    private int FindAverage(int[] nums)
    {
        var sum = nums.Sum();
        if (sum % 2 != 0) return -1;
        return sum / 2;
    }

    class CurrentSelection
    {
        private BitArray _selection;

        public CurrentSelection(int n)
        {
            _selection = new BitArray(n);
        }

        public void Select(int index)
        {
            _selection.Set(index, true);
        }

        public void Unselect(int index)
        {
            _selection.Set(index, false);
        }

        public bool IsSelected(int index)
        {
            return _selection[index];
        }

        public string GetKey(int[] nums)
        {
            var stringBuilder = new StringBuilder();
            var foundMap = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (IsSelected(i))
                {
                    if (foundMap.TryGetValue(nums[i], out int currentSum))
                    {
                        foundMap[nums[i]] += currentSum;
                    }
                    else
                    {
                        foundMap.Add(nums[i], currentSum);
                    }
                }
            }
            foreach (var item in foundMap.OrderByDescending(it => it.Key))
            {
                stringBuilder.Append($",{item.Key}x{item.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}