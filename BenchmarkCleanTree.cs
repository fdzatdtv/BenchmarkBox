using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    [MemoryDiagnoser]
    public class BenchmarkCleanTree
    {
        private BinaryTree<object> _tree;
        private int _nodeCount;
        private void InitTree()
        {
            _tree = new BinaryTree<object>();
            const int maxLevel = 10;
            var parentNodes = new BinaryTreeNode<object>[1];
            parentNodes[0] = _tree.Root = NewNode();
            for (int level = 1; level < maxLevel; level++)
            {
                var levelNodes = new BinaryTreeNode<object>[1 << level];
                for (int index = 0; index < (1 << level); index++)
                {
                    levelNodes[index] = NewNode();
                }

                int nodeIndex = 0;
                
                foreach (var parent in parentNodes)
                {
                    parent.Left = levelNodes[nodeIndex++];
                    parent.Right = levelNodes[nodeIndex++];
                }
                
                parentNodes = levelNodes;
            }
        }

        private BinaryTreeNode<object> NewNode()
        {
            _nodeCount++;
            return new BinaryTreeNode<object>();
        }

        public int NodeCount => _nodeCount;
        
        public BenchmarkCleanTree()
        {
            InitTree();
        }

        [Benchmark(Baseline = true)]
        public void CleanRecursive()
        {
            CleanNodeRecursive(_tree.Root);
            _tree.Root = null;
        }
        
        [Benchmark]
        public void CleanRoot()
        {
            _tree.Root = null;
        }

        private void CleanNodeRecursive(BinaryTreeNode<object> node)
        {
            if (node == null)
                return;
            CleanNodeRecursive(node.Left);
            CleanNodeRecursive(node.Right);
            node.Left = null;
            node.Right = null;
        }
    }
}