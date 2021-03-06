﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MeshTech.Model.Network
{
    public class AliveTreeNodeEnumerator : IEnumerator<TreeNode>
    {
        private readonly IEnumerator<TreeNode> source;

        public AliveTreeNodeEnumerator(IEnumerator<TreeNode> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = source;
        }

        public void Dispose()
        {
            source.Dispose();
        }

        public bool MoveNext()
        {
            {
                bool result;
                bool isMovedNext;
                do
                {
                    isMovedNext = source.MoveNext();
                    result = isMovedNext && source.Current != null;
                }
                while (!result && isMovedNext);

                return result;
            }
        }

        public void Reset()
        {
            source.Dispose();
        }

        public TreeNode Current
        {
            get { return source.Current; }
        }

        object IEnumerator.Current => Current;
    }
}
