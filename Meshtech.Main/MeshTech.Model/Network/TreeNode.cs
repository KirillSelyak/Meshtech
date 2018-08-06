using System.Collections;
using System.Collections.Generic;

namespace MeshTech.Model.Network
{
    public class TreeNode : IEnumerable<TreeNode>
    {
        private Beacon beacon;
        private TreeNode[] _childTreeNodes = new TreeNode[8];

        public TreeNode(Beacon beacon)
        {
            this.beacon = beacon;
        }

        public Beacon Beacon
        {
            get
            {
                return beacon;
            }
        }

        public TreeNode this[int index]
        {
            get
            {
                var result = _childTreeNodes[index];
                return result;
            }
            set { _childTreeNodes[index] = value; }
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            var originalEnumerator = ((IEnumerable<TreeNode>)_childTreeNodes).GetEnumerator();
            var result = new AliveTreeNodeEnumerator(originalEnumerator);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
