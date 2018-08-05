using System;
using System.Collections.Generic;
using System.Linq;

namespace MeshTech.Model.Network
{
    public class MeshtechTreeConstructor
    {
        private const int IgnoreLastCellCount = 1;

        public TreeNode Construct(IEnumerable<Beacon> beacons)
        {
            if (beacons == null)
                throw new ArgumentNullException(nameof(beacons));

            var result = CreateRoot("DC73267C3630");
            foreach (var currentBeacon in beacons.Where(o => !string.IsNullOrEmpty(o.MacAddress)))
            {
                var currentNode = result;
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
            return result;
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

        private static TreeNode CreateRoot(string gateWay)
        {
            var beacond = new Beacon
            {
                Route = OctRoute.Parse("FFFFFFFFFFF8"),
                MacAddress = gateWay
            };
            var result = new TreeNode(beacond);
            return result;
        }
    }
}
