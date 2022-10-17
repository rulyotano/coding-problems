public class BearAndSteadyGene
{

    /*
     * Complete the 'steadyGene' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING gene as parameter.
     */

    public static int steadyGene(string gene)
    {
        var expectedAmount = gene.Length / 4;
        var counts = CountGenes(gene);
        var difference = counts.Difference(expectedAmount);
        var amountToSearch = difference.GetAmountToSearch();

        var (currentCount, i) = FindFirstValidIndex(gene, amountToSearch);
        var minLen = i + 1;
        var tail = 0;
        while (i < gene.Length)
        {
            while (tail < gene.Length && amountToSearch.IsSatifiedBy(currentCount.AddClone(gene[tail], -1)))
            {
                currentCount.Add(gene[tail], - 1);
                tail++;
            }

            var size = i - tail + 1;
            if (minLen > size) minLen = size;

            i++;
            if (i < gene.Length)
            {
                currentCount.Add(gene[i]);
            }
        }

        return minLen;
    }

    private static (GeneCount currentCount, int i) FindFirstValidIndex(string gene, GeneCount amountToSearch)
    {
        var currentCount = new GeneCount();
        var i = -1;
        while (!amountToSearch.IsSatifiedBy(currentCount))
        {
            i++;
            currentCount.Add(gene[i]);
        }
        return (currentCount, i);
    }

    private static GeneCount CountGenes(string gene)
    {
        var geneCounts = new GeneCount();
        for (int i = 0; i < gene.Length; i++)
        {
            geneCounts.Add(gene[i]);
        }
        return geneCounts;
    }

    public class GeneCount
    {
        public int A { get; set; }
        public int C { get; set; }
        public int G { get; set; }
        public int T { get; set; }
        public void Add(char c, int value = 1)
        {
            switch (c)
            {
                case 'A':
                    A += value;
                    break;
                case 'C':
                    C += value;
                    break;
                case 'G':
                    G += value;
                    break;
                case 'T':
                    T += value;
                    break;
            }
        }

        public GeneCount AddClone(char c, int value = 1)
        {
            var result = new GeneCount { A = A, C = C, G = G, T = T };
            result.Add(c, value);
            return result;
        }

        public GeneCount Difference(int expectedAmount)
        {
            return new GeneCount
            {
                A = expectedAmount - A,
                C = expectedAmount - C,
                G = expectedAmount - G,
                T = expectedAmount - T,
            };
        }

        public GeneCount GetAmountToSearch()
        {
            return new GeneCount
            {
                A = A < 0 ? A * -1 : 0,
                C = C < 0 ? C * -1 : 0,
                G = G < 0 ? G * -1 : 0,
                T = T < 0 ? T * -1 : 0,
            };
        }

        public bool IsSatifiedBy(GeneCount currentCount)
        {
            return currentCount.A >= A && currentCount.C >= C && currentCount.G >= G && currentCount.T >= T;
        }
    }
}

