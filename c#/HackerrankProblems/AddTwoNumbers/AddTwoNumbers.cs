public partial class Solution
{
  public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
  {
    var result = new ListNode();
    var currentResult = result;
    var currentL1 = l1;
    var currentL2 = l2;
    var carriage = false;

    while (currentL1 is not null || currentL2 is not null)
    {
      var currentSum = (currentL1?.val ?? 0) + (currentL2?.val ?? 0) + (carriage ? 1 : 0);
      currentResult.val = currentSum % 10;
      carriage = currentSum / 10 == 1;
      // move next
      currentL1 = currentL1?.next;
      currentL2 = currentL2?.next;
      if (currentL1 is not null || currentL2 is not null)
      {
        var newCurrentResult = new ListNode(0);
        currentResult.next = newCurrentResult;
        currentResult = newCurrentResult;
      }
    }

    if (carriage)
    {
        currentResult.next = new ListNode(1);
    }

    return result;
  }
}

public class ListNode
{
  public int val;
  public ListNode next;
  public ListNode(int val = 0, ListNode next = null)
  {
    this.val = val;
    this.next = next;
  }
}