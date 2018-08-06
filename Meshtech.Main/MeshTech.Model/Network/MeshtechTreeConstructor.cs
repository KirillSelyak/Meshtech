using System;
using System.Collections.Generic;
using System.Linq;

namespace MeshTech.Model.Network
{
    public class MeshtechTreeConstructor
    {
        private const int IgnoreLastCellCount = 1;

        public IEnumerable<TreeNode> Construct(IEnumerable<Beacon> beacons)
        {
            if (beacons == null)
                throw new ArgumentNullException(nameof(beacons));

            var networks = new Dictionary<string, TreeNode>();
            foreach (var currentBeacon in beacons.Where(b => !b.Equals(Beacon.InvalidBeacon)))
            {
                TreeNode network = GetOrCreateRoot(networks, currentBeacon.Gateway);
                if (string.IsNullOrEmpty(currentBeacon.MacAddress))
                    continue;

                int? deepestIndex = FindDeepestIndex(currentBeacon);
                if (deepestIndex.HasValue)
                {
                    AddNodes(network, currentBeacon, deepestIndex.Value);
                }
            }
            return networks.Values;
        }

        private void AddNodes(TreeNode gateway, Beacon beacon, int deepestIndex)
        {
            var currentNode = gateway;
            for (int j = 0; j <= deepestIndex; j++)
            {
                var nodeIndex = beacon.Route[j];
                if (currentNode[nodeIndex] == null)
                {
                    var newBeacon = new Beacon();
                    newBeacon.Route = currentNode.Beacon.Route.Insert(j, nodeIndex);
                    newBeacon.Gateway = currentNode.Beacon.Gateway;
                    currentNode[nodeIndex] = new TreeNode(newBeacon);
                }
                currentNode = currentNode[nodeIndex];
            }
            currentNode.Beacon.MacAddress = beacon.MacAddress;
        }

        private static int? FindDeepestIndex(Beacon beacon)
        {
            for (int i = beacon.Route.Length - 1 - IgnoreLastCellCount; i >= 0; i--)
            {
                if (beacon.Route[i] != OctRoute.MaxCellValue)
                {
                    return i;
                }
            }
            return null;
        }

        private static TreeNode CreateRoot(string gateway)
        {
            var beacond = new Beacon
            {
                Route = OctRoute.Parse("FFFFFFFFFFF8"),
                MacAddress = gateway,
                Gateway = gateway

            };
            var result = new TreeNode(beacond);
            return result;
        }

        private static TreeNode GetOrCreateRoot(Dictionary<string, TreeNode> networks, string gateway)
        {
            networks.TryGetValue(gateway, out var result);
            if (result == null)
            {
                result = CreateRoot(gateway);
                networks.Add(gateway, result);
            }
            return result;
        }
    }
}
