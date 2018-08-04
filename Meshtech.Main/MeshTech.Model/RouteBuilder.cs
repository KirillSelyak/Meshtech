namespace MeshTech.Model
{
    public class RouteNode
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

        public RouteNode[] ChildRouteNodes
        {
            get { return childRouteNodes; }
        }

    }
}
