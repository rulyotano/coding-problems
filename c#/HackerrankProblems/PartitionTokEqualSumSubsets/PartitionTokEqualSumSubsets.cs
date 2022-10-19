
public class SolutionPartitionKSubset
{
    public bool CanPartitionKSubsets(int[] nums, int k)
    {
        Array.Sort(nums);
        var average = Average(nums, k);
        if (average == -1) return false;
        var possibleSets = FindPossibleSets(average, nums);
        var possibleSetsByIndex = new Dictionary<int, Set>();
        for (int i = 0; i < possibleSets.Count; i++)
        {
            possibleSetsByIndex.Add(i, possibleSets[i]);
        }

        return CanGetSetsHavingAllAndNotRepeating(possibleSetsByIndex, k, nums.Length);
    }

    private bool CanGetSetsHavingAllAndNotRepeating(Dictionary<int, Set> possibleSets, int k, int n)
    {
        if (possibleSets.Count < k) return false;
        var selectedSet = new Set(n);
        var availAbleSets = possibleSets;
        var selectedAmount = 0;

        bool IsFeasibleSolution(KeyValuePair<int, Set>[] fittingAvailableSets)
        {
            if (fittingAvailableSets.Length + selectedAmount < k) return false;
            var checkSet = selectedSet.Clone();
            foreach (var fittingAvailableSet in fittingAvailableSets)
            {
                checkSet.Add(fittingAvailableSet.Value);
                if (checkSet.IsComplete()) return true;
            }
            return false;
        }

        bool CanGetSetRecurive()
        {
            if (selectedAmount == k) return selectedSet.IsComplete();
            var fittingAvailableSets = availAbleSets.Where(s => selectedSet.Fit(s.Value)).ToArray();
            if (!IsFeasibleSolution(fittingAvailableSets)) return false;
            foreach (var currentSet in fittingAvailableSets)
            {
                selectedSet.Add(currentSet.Value);
                availAbleSets.Remove(currentSet.Key);
                selectedAmount++;
                if (CanGetSetRecurive()) return true;
                selectedSet.Remove(currentSet.Value);
                availAbleSets.Add(currentSet.Key, currentSet.Value);
                selectedAmount--;
            }
             return false;
        }

        return CanGetSetRecurive();
    }

    private int Average(int[] nums, int k)
    {
        var sum = nums.Sum();
        if (sum % k != 0) return -1;
        return sum / k;
    }

    private List<Set> FindPossibleSets(int average, int[] nums)
    {
        var possibleSets = new List<Set>();
        var currentSet = new Set(nums.Length);
        var currentSum = 0;
        
        void FindPossibleSetsRecursiveAddingIndex(int currentIndex)
        {
            var current = nums[currentIndex];
            if (currentSum + current > average) return;

            void AddCurrent()
            {
                currentSet.Add(currentIndex);
                currentSum += current;
            }
            void RemoveCurrent()
            {
                currentSet.Remove(currentIndex);
                currentSum -= current;
            }

            AddCurrent();

            if (average == currentSum)
            {
                possibleSets.Add(currentSet.Clone());
                RemoveCurrent();
                return;
            }

            for (int nextIndex = currentIndex + 1; nextIndex < nums.Length; nextIndex++)
            {
                var nextValue = nums[nextIndex];
                if (currentSum + nextValue > average)
                {
                    RemoveCurrent();
                    return;
                }
                FindPossibleSetsRecursiveAddingIndex(nextIndex);
            }

            RemoveCurrent();
        }

        for (int i = 0; i < nums.Length; i++)
        {
            FindPossibleSetsRecursiveAddingIndex(i);
        }

        return possibleSets;
    }

    public class Set
    {
        private int _mask = 0;
        private int _completeMask;

        private Set() { }

        public Set(int amount)
        {
            _completeMask = (1 << amount) - 1;
        }

        public bool Fit(int itemIndex)
        {
            var checkMask = 1 << itemIndex;
            return (_mask & checkMask) == 0;
        }

        public void Add(int itemIndex)
        {
            var addMask = 1 << itemIndex;
            _mask |= addMask;
        }

        public void Remove(int indexItem)
        {
            var removeMask = ~(1 << indexItem);
            _mask &= removeMask;
        }

        public void Add(Set otherSet)
        {
            _mask |= otherSet._mask;
        }

        public void Remove(Set otherSet)
        {
            var removeMask = ~(otherSet._mask);
            _mask &= removeMask;
        }

        public bool Fit(Set otherSet)
        {
            return (_mask & otherSet._mask) == 0;
        }

        public bool IsComplete() => (_mask & _completeMask) == _completeMask;

        public Set Clone() => new Set() { _mask = _mask, _completeMask = _completeMask };

    }
}

