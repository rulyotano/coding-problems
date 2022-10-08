public partial class Solution
{
  public int[] Intersect(int[] nums1, int[] nums2)
  {
    var cache = new Dictionary<int, Presence>();
    for (int i = 0; i < nums1.Length; i++)
    {
      Presence presence = null;
      if (!cache.TryGetValue(nums1[i], out presence))
      {
        presence = new Presence();
        cache.Add(nums1[i], presence);
      }
      presence.TimesIn1++;
    }

    for (int i = 0; i < nums2.Length; i++)
    {
      Presence presence = null;
      if (!cache.TryGetValue(nums2[i], out presence))
      {
        presence = new Presence();
        cache.Add(nums2[i], presence);
      }
      presence.TimesIn2++;
    }

    var result = new List<int>();
    foreach (var presence in cache)
    {
      var amount = Math.Min(presence.Value.TimesIn1, presence.Value.TimesIn2);
      while (amount > 0)
      {
        result.Add(presence.Key);
        amount--;
      }
    }

    return result.ToArray();
  }
}

public class Presence
{
  public int TimesIn1 { get; set; }

  public int TimesIn2 { get; set; }
}