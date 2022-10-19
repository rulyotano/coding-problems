using System.Collections;

public class JumGameIIISolution
{
    public bool CanReach(int[] arr, int start)
    {
        var marks = new BitArray(arr.Length);
        var nextNodes = new Queue<int>();

        nextNodes.Enqueue(start);
        marks[start] = true;

        while (nextNodes.Count > 0)
        {
            var currentIndex = nextNodes.Dequeue();
            if (arr[currentIndex] == 0) return true;
            var leftIndex = currentIndex - arr[currentIndex];
            var rightIndex = currentIndex + arr[currentIndex];
            if (leftIndex >= 0 && !marks[leftIndex])
            {
                nextNodes.Enqueue(leftIndex);
                marks[leftIndex] = true;
            }
            if (rightIndex < arr.Length && !marks[rightIndex])
            {
                nextNodes.Enqueue(rightIndex);
                marks[rightIndex] = true;
            }
        }
        return false;
    }
}