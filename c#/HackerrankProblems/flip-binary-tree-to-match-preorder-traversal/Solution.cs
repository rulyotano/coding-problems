namespace flip.binary.tree.to.match.preorder.traversal;

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
    private const int NotFound = -1;
    public IList<int> FlipMatchVoyage(TreeNode root, int[] voyage) 
    {
        var currentVoyage = new Voyage(voyage);
        if (root.val != currentVoyage.Current) return new List<int> { NotFound };
        currentVoyage.Move();
        return MatchVoyage(root, currentVoyage);
    }

    public IList<int> MatchVoyage(TreeNode node, Voyage voyage)
    {
        var result = new List<int>();
        if (node.left is not null)
        {
            if (node.left.val != voyage.Current)
            {
                var tempChild = node.left;
                node.left = node.right;
                node.right = tempChild;
                result.Add(node.val);
            }
            if (node.left is null || node.left.val != voyage.Current)
            {
                return new List<int> { NotFound };
            }
            voyage.Move();
            var leftResult = MatchVoyage(node.left, voyage);
            if (leftResult.Contains(NotFound)) return leftResult;
            foreach (var item in leftResult) result.Add(item);
        }
        if (node.right is not null)
        {
            if (node.right.val != voyage.Current) return new List<int> { NotFound };
            voyage.Move();
            var rightResult = MatchVoyage(node.right, voyage);
            if (rightResult.Contains(NotFound)) return rightResult;
            foreach (var item in rightResult) result.Add(item);
        }

        return result;
    }

    public class Voyage
    {
        private int _voyageIndex = 0;
        private int[] _voyage;

        public Voyage(int[] voyage)
        {
            _voyage = voyage;
        }

        public int Current => _voyage[_voyageIndex];
        public void Move() => _voyageIndex++;
    }
}