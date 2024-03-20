namespace BenchmarkBox
{
    public sealed class BinaryTree<TNodeType>
    {
        public BinaryTreeNode<TNodeType> Root { get; set; }
    }

    public sealed class BinaryTreeNode<TNodeType>
    {
        public TNodeType Node { get; set; }
        public BinaryTreeNode<TNodeType> Left { get; set; }
        public BinaryTreeNode<TNodeType> Right { get; set; }
    }
}