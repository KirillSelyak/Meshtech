using System;
using System.Collections.Generic;

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
            foreach (var currentBeacon in beacons)
            {
                TreeNode currentNode = GetAndCreateRoot(networks, currentBeacon);
                if (string.IsNullOrEmpty(currentBeacon.MacAddress))
                    continue;
                int? i = FindCellIndex(currentBeacon);
                if (i.HasValue)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        var nodeIndex = currentBeacon.Route[j];
                        if (currentNode[nodeIndex] == null)
                        {
                            var newBeacon = new Beacon();
                            newBeacon.Route = currentNode.Beacon.Route.Insert(j, nodeIndex);
                            currentNode[nodeIndex] = new TreeNode(newBeacon);
                        }
                        currentNode = currentNode[nodeIndex];
                    }
                    currentNode.Beacon.MacAddress = currentBeacon.MacAddress;
                }
            }
            return networks.Values;
        }

        private static int? FindCellIndex(Beacon beacon)
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

        private static TreeNode GetAndCreateRoot(Dictionary<string, TreeNode> networks, Beacon beacon)
        {
            networks.TryGetValue(beacon.Gateway, out var result);
            if (result == null)
            {
                result = CreateRoot(beacon.Gateway);
                networks.Add(beacon.Gateway, result);
            }

            return result;
        }
    }
}
