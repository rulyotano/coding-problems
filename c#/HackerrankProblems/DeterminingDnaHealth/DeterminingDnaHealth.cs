public class DeterminingDnaHealth
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Gene[] genes = Console.ReadLine().TrimEnd().Split(' ').Select((g, i) => new Gene { Value = g, Index = i }).ToArray();
        Console.ReadLine().TrimEnd().Split(' ').Select((healthTemp, i) =>
        {
            genes[i].Health = Convert.ToUInt32(healthTemp);
            return 1;
        }).ToList();

        var genesTrie = BuildTrie(genes);

        int s = Convert.ToInt32(Console.ReadLine().Trim());

        var maxHealthSum = 0UL;
        var minHealthSum = ulong.MaxValue;
        for (int sItr = 0; sItr < s; sItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int first = Convert.ToInt32(firstMultipleInput[0]);

            int last = Convert.ToInt32(firstMultipleInput[1]);

            string d = firstMultipleInput[2];

            var currentHealthSum = 0UL;

            for (int currentIndex = 0; currentIndex < d.Length; currentIndex++)
            {
                var currentLookForwardIndex = currentIndex;
                var currentCharacter = d[currentLookForwardIndex];
                var currentTrie = genesTrie.GetNode(currentCharacter);
                while (currentTrie != null && currentLookForwardIndex < d.Length)
                {
                    if (currentTrie.IsMatch)
                    {
                        currentHealthSum += CalculateMatchValue(currentTrie, first, last);
                    }

                    currentLookForwardIndex++;
                    if (currentLookForwardIndex < d.Length)
                    {
                        currentCharacter = d[currentLookForwardIndex];
                        currentTrie = currentTrie.GetNode(currentCharacter);
                    }
                }
            }

            if (maxHealthSum < currentHealthSum)
            {
                maxHealthSum = currentHealthSum;
            }
            if (minHealthSum > currentHealthSum)
            {
                minHealthSum = currentHealthSum;
            }
        }
        Console.WriteLine($"{minHealthSum} {maxHealthSum}");
    }

    private static ulong CalculateMatchValue(Trie<GeneList> currentTrie, int first, int last)
    {
        Func<Gene, int> GetCompareFunction(int indexToSearch) => current => current.Index - indexToSearch;
        var matchingGenes = currentTrie.Value.Genes;
        if (last < matchingGenes[0].Index) return 0;
        if (matchingGenes[matchingGenes.Count - 1].Index < first) return 0;
        if (first == last)
        {
            var matchingDnaIndex = SortedArray.BinarySearch(matchingGenes, GetCompareFunction(first));
            return matchingDnaIndex == SortedArray.NotFound ? 0 : matchingGenes[matchingDnaIndex].Health;
        }
        var firstMatchingIndex = SortedArray.FindInsertIndex(matchingGenes, GetCompareFunction(first));
        var lastMatchingIndex = SortedArray.FindInsertIndex(matchingGenes, GetCompareFunction(last));
        var finalLastMatchingIndex = lastMatchingIndex == matchingGenes.Count || matchingGenes[lastMatchingIndex].Index > last ? lastMatchingIndex - 1 : lastMatchingIndex;
        if (firstMatchingIndex > finalLastMatchingIndex) return 0;
        if (firstMatchingIndex == finalLastMatchingIndex)
        {
            var matchingGene = matchingGenes[firstMatchingIndex];
            if (first <= matchingGene.Index && matchingGene.Index <= last) return matchingGene.Health;
            return 0;
        }
        var firstGeneMatching = matchingGenes[firstMatchingIndex];
        var lastGeneMatching = matchingGenes[finalLastMatchingIndex];
        return lastGeneMatching.AccumulativeHealth - firstGeneMatching.AccumulativeHealth + firstGeneMatching.Health;
    }

    private static Trie<GeneList> BuildTrie(Gene[] genes)
    {
        var genesTrie = new Trie<GeneList>();
        
        foreach (Gene gene in genes)
        {
            var geneList = new GeneList(gene);
            genesTrie.Add(gene.Value, geneList);
        }

        return genesTrie;
    }

    public class Gene
    {
        public int Index { get; set; }

        public string Value { get; set; }

        public uint Health { get; set; }

        public ulong AccumulativeHealth { get; set; }
    };

    public class GeneList : ITrieItem
    {
        public GeneList(Gene gene)
        {
            Genes = new List<Gene> { gene };
        }
        public List<Gene> Genes { get; }
        public ITrieItem ResolveCollision(ITrieItem existingItem)
        {
            var existingList = (GeneList) existingItem;
            var currentGene = Genes[0];
            currentGene.AccumulativeHealth =
                currentGene.Health + existingList.Genes[^1].AccumulativeHealth;
            existingList.Genes.Add(currentGene);
            return existingList;
        }
    }

    public interface ITrieItem
    {
        ITrieItem ResolveCollision(ITrieItem existingItem);
    }

    public class Trie<T> where T : class, ITrieItem
    {
        public T Value { get; private set; }
        public Trie<T>[] Children { get; }
        private int _childrenCount = 0;
        public bool IsLeaf => _childrenCount == 0;
        public bool IsMatch => Value is not null;

        public Trie()
        {
            Children = new Trie<T>['z' - 'a' + 1];
        }

        public void Add(string key, ITrieItem treeItem)
            => AddPrivate(key, treeItem, 0);

        public T Get(string key)
        {
            var node = GetNode(key);
            return node?.Value;
        }

        public Trie<T> GetNode(string key)
            => GetNodePrivate(key);

        public Trie<T> GetNode(char keyCharacter)
        {
            return Children[keyCharacter - 'a'];
        }

        private void AddPrivate(string key, ITrieItem treeItem, int currentIndex = 0)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (currentIndex == key.Length)
            {
                Value = (T)(IsMatch ? treeItem.ResolveCollision(Value) : treeItem);
                return;
            }
            var currentCharacter = key[currentIndex];
            var currentChildIndex = currentCharacter - 'a';
            var child = Children[currentChildIndex];

            if (child == null)
            {
                child = new Trie<T>();
                Children[currentChildIndex] = child;
            }

            child.AddPrivate(key, treeItem, currentIndex + 1);
        }

        private Trie<T> GetNodePrivate(string key, int currentIndex = 0)
        {
            if (string.IsNullOrEmpty(key)) return null;
            if (currentIndex == key.Length)
            {
                return this;
            }
            var currentCharacter = key[currentIndex];
            var child = Children[currentCharacter - 'a'];
            if (child == null)
            {
                return null;
            }

            return child.GetNodePrivate(key, currentIndex + 1);
        }
    }

    public static class SortedArray
    {
        public const int NotFound = -1;

        public static int BinarySearch<T>(IList<T> collection, Func<T, int> compareFunction)
        {
            var (foundIndex, _) = BinarySearch(collection, compareFunction, 0, collection.Count - 1);
            return foundIndex;
        }

        public static int FindInsertIndex<T>(IList<T> collection, Func<T, int> compareFunction)
        {
            if (collection.Count == 0) return 0;
            var (_, bestIndex) = BinarySearch(collection, compareFunction, 0, collection.Count - 1);
            return bestIndex;
        }

        private static (int foundIndex, int bestIndex) BinarySearch<T>(IList<T> collection, Func<T, int> compareFunction, int start, int end)
        {
            if (start > end) return (NotFound, NotFound);
            if (start == end)
            {
                var whenEqualsComapreResult = compareFunction(collection[start]);
                if (whenEqualsComapreResult == 0) return (start, start);
                var currentIsGreater = whenEqualsComapreResult > 0;
                return currentIsGreater ? (NotFound, start) : (NotFound, start + 1);
            }
            if (end - start == 1)
            {
                var compareStartResult = compareFunction(collection[start]);
                if (compareStartResult == 0) return (start, start);
                var compareEndResult = compareFunction(collection[end]);
                if (compareEndResult == 0) return (end, end);
                var isLowerThanStart = compareStartResult > 0;
                if (isLowerThanStart) return (NotFound, start);
                var isGreaterThanEnd = compareEndResult < 0;
                if (isGreaterThanEnd) return (NotFound, end + 1);
                return (NotFound, start + 1);
            }

            var middIndex = (start + end) / 2;
            var middElement = collection[middIndex];
            var compareResult = compareFunction(middElement);
            if (compareResult == 0)
            {
                return (middIndex, middIndex);
            }

            if (compareResult > 0)
            {
                return BinarySearch(collection, compareFunction, start, middIndex);
            }

            return BinarySearch(collection, compareFunction, middIndex, end);
        }
    }
}