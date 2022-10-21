public class TreeNode
{
  public int val;
  public TreeNode left;
  public TreeNode right;
  public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
  {
    this.val = val;
    this.left = left;
    this.right = right;
  }
}

public class CountGoodNodesInBinaryTreeSolution
{
  public int GoodNodes(TreeNode root)
  {
    return CountOfGoodNodes(root, int.MinValue);
  }

  private int CountOfGoodNodes(TreeNode currentNode, int maxValue)
  {
    if (currentNode is null) return 0;
    if (maxValue > currentNode.val) return CountOfGoodNodes(currentNode.left, maxValue) + CountOfGoodNodes(currentNode.right, maxValue);
    return 1 + CountOfGoodNodes(currentNode.left, currentNode.val) + CountOfGoodNodes(currentNode.right, currentNode.val);
  }
}