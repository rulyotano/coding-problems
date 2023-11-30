/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */

 // src = leetcode
namespace cousins.iin.binary.tree.ii;
 
public class Solution {
    private int[] _levelSum;

    public TreeNode ReplaceValueInTree(TreeNode root) {
        _levelSum = new int[1000000];

        CalculateLevelSum(0, root);

        return BuildChildCousinTree(root);        
    }

    private void CalculateLevelSum(int level, TreeNode currentNode)
    {
        _levelSum[level] += currentNode.val;
        
        if (currentNode.left is not null)
            CalculateLevelSum(level + 1, currentNode.left);
        if (currentNode.right is not null)
            CalculateLevelSum(level + 1, currentNode.right);
    }

    private TreeNode BuildChildCousinTree(TreeNode currentOriginNode, TreeNode currentNewNode = null, int level = 0)
    {
        var result = currentNewNode is null ? new TreeNode { val = 0 } : currentNewNode;
        var nextNevel = level + 1;
        var childLevelSum = _levelSum[nextNevel];
        var childSum = (currentOriginNode.left?.val ?? 0) + (currentOriginNode.right?.val ?? 0);

        if (currentOriginNode.left is not null)
        {
            result.left = new TreeNode { val = childLevelSum - childSum };
            BuildChildCousinTree(currentOriginNode.left, result.left, nextNevel);
        }
        if (currentOriginNode.right is not null)
        {
            result.right = new TreeNode { val = childLevelSum - childSum };
            BuildChildCousinTree(currentOriginNode.right, result.right, nextNevel);
        }

        return result;
    }
}