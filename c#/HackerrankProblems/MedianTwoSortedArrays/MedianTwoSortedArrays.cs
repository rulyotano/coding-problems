public class SolutionMedianTwoSortedArrays
{
  public double FindMedianSortedArrays(int[] nums1, int[] nums2)
  {
    var allNums = new int[nums1.Length + nums2.Length];

    var i = 0;
    var j = 0;
    while (i < nums1.Length || j < nums2.Length)
    {
      if (j < nums2.Length && (i >= nums1.Length || nums2[j] < nums1[i]))
      {
        allNums[i + j] = nums2[j];
        j++;
        continue;
      }
      allNums[i + j] = nums1[i];
      i++;
    }

    if (allNums.Length == 0) return 0;

    return allNums.Length % 2 == 1 ? allNums[allNums.Length / 2] : (allNums[allNums.Length / 2] + allNums[allNums.Length / 2 - 1]) / 2.0;
  }
}