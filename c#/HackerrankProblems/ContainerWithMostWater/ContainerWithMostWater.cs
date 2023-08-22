public class ContainerWithMostWaterSolution {
    public int MaxArea(int[] height) {
        var bigger = 0;
        var li = 0;
        var ri = height.Length - 1;
        while (li < ri)
        {
            var current = Math.Min(height[li], height[ri])*(ri - li);
            if (bigger < current) bigger = current;
            if (height[li] <= height[ri]) li++;
            else ri--;
        }
        return bigger;
    }
}