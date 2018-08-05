using System;
using System.Collections;
using System.Collections.Generic;

namespace MeshTech.Model.Network
{
    public class RouteNode : IEnumerable<RouteNode>
    {
        private Beacon beacon;
        private RouteNode[] childRouteNodes = new RouteNode[8];

        public RouteNode(Beacon beacon)
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

        [Obsolete]
        public RouteNode AddOrUpdate(byte index, Beacon targetBeacon)
        {
            var childRoute = childRouteNodes[index];
            if (childRoute == null)
            {
                childRoute = new RouteNode(targetBeacon);
                childRouteNodes[index] = childRoute;
            }
            return childRoute;
        }

        public RouteNode this[int index]
        {
            get
            {
                var result = childRouteNodes[index];
                return result;
            }
            set { childRouteNodes[index] = value; }
        }

        public IEnumerator<RouteNode> GetEnumerator()
        {
            var originalEnumerator = ((IEnumerable<RouteNode>)childRouteNodes).GetEnumerator();
            var result = new RouteNodeEnumerator(originalEnumerator);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
