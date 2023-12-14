namespace sort.the.students.by.their.kth.score;

// src: leet code

public class Solution {
    public int[][] SortTheStudents(int[][] score, int k) {
        var examResultByIndex = new ExamIndex[score.Length];
        for (var i = 0; i < score.Length; i++)
        {
            examResultByIndex[i] = new ExamIndex(score[i][k], i);
        }

        BubbleSort(examResultByIndex);

        for (var j = 0; j < score[0].Length; j++)
        {
            var t = new int[score.Length];
            for (var i = 0; i < examResultByIndex.Length; i++)
            {
                t[i] = score[examResultByIndex[i].Index][j];
            }

            for (var i = 0; i < score.Length; i++)
            {
                score[i][j] = t[i];
            }
        }

        return score;
    }

    private void BubbleSort(ExamIndex[] items)
    {
        for (var i = 1; i < items.Length; i++)
        {
            var j = i;
            while (j > 0 && items[j].ExamResult > items[j-1].ExamResult)
            {
                var t = items[j];
                items[j] = items[j-1];
                items[j-1] = t;
                j--;
            }
        }
    }

    private record ExamIndex(int ExamResult, int Index);
}