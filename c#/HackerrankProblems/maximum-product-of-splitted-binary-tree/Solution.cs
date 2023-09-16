namespace maximum.product.of.splitted.binary.tree;
// src: leetcode

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution {
    public int MaxProduct(TreeNode root) {
        var maxSumTree = TreeNodeSum.BuildTreeNodeSum(root);
        long best = 0;

        var queue = new Queue<TreeNodeSum>();
        queue.Enqueue(maxSumTree);

        while (queue.Count > 0)
        {
          var current = queue.Dequeue();
          if (current.left is not null) queue.Enqueue(current.left);
          if (current.right is not null) queue.Enqueue(current.right);
          var currentBest = current.CalculateNodeBest(maxSumTree.sum);
          if (best < currentBest) best = currentBest;
        }

        return (int)(best % (1000000007));
    }
}

public class TreeNodeSum {
  public int sum;
  public int val;
  public TreeNodeSum left;
  public TreeNodeSum right;

  public static TreeNodeSum BuildTreeNodeSum(TreeNode root)
  {
    if (root is null) return null;
    var result = new TreeNodeSum { val = root.val };

    result.left = TreeNodeSum.BuildTreeNodeSum(root.left);
    result.right = TreeNodeSum.BuildTreeNodeSum(root.right);

    result.sum = root.val + (result.left?.sum ?? 0) + (result.right?.sum ?? 0);

    return result;
  }

  public long CalculateNodeBest(int rootSum) {
    long rightValue = (rootSum - sum + val + (left?.sum ?? 0)) * (long) (right?.sum ?? 0);
    long leftValue = (rootSum - sum + val + (right?.sum ?? 0)) * (long) (left?.sum ?? 0);
    return Math.Max(rightValue, leftValue);
  }
}