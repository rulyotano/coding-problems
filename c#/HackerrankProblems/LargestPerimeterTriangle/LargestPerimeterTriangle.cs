public partial class Solution
{
  public int LargestPerimeter(int[] nums)
  {
    var numsOredered = new Queue<int>(nums.OrderByDescending(n => n));
    var threeLargest = new Queue<int>();
    threeLargest.Enqueue(numsOredered.Dequeue());
    threeLargest.Enqueue(numsOredered.Dequeue());
    threeLargest.Enqueue(numsOredered.Dequeue());
    while(threeLargest.Count == 3 && !IsValidTriangle(threeLargest.First(), threeLargest.Skip(1).Take(1).First(), threeLargest.Skip(2).Take(1).First()))
    {
      threeLargest.Dequeue();
      if (numsOredered.Count > 0) threeLargest.Enqueue(numsOredered.Dequeue());
    }
    if (threeLargest.Count < 3) return 0;
    return threeLargest.Sum();
  }

  private bool IsValidTriangle(int a, int b, int c) => a < b + c && b < a + c && c < a + b;
}