
class DirectorOfPhotographySolution
{

    public int getArtisticPhotographCount(int N, string C, int X, int Y)
    {
        C = C.ToUpper();
        var maxI = N - 1 - 2 * X;
        var count = 0;
        for (int i = 0; i <= maxI; i++)
        {
            if (C[i] != 'p' && C[i] != 'B') continue;
            var nextToFind = C[i] == 'p' ? 'B' : 'p';
            var maxJ = Math.Min(i + Y, N - 1);
            for (int j = i + X; j <= maxJ; j++)
            {
                if (C[j] != 'A') continue;
                var maxK = Math.Min(j + Y, N - 1);
                for (int k = j + X; k <= maxK; k++)
                {
                    if (C[k] == nextToFind) count++;
                }
            }

        }
        return count;
    }

}
