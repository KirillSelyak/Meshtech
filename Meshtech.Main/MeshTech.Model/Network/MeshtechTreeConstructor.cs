using System;
using System.Collections.Generic;
using System.Linq;

namespace MeshTech.Model.Network
{
    public class MeshtechTreeConstructor
    {
        private const int IgnoreLastCellCount = 1;

        public RouteNode Construct(IEnumerable<Beacon> beacons)
        {
            if (beacons == null)
                throw new ArgumentNullException(nameof(beacons));

            var result = CreateRoot("DC73267C3630");
            foreach (var currentBeacon in beacons.Where(o => !string.IsNullOrEmpty(o.MacAddress)))
            {
                var currentNode = result;
                for (int i = currentBeacon.Route.Length - 1 - IgnoreLastCellCount; i >= 0; i--)
                {
                    var cellValue = currentBeacon.Route[i];
                    if (cellValue != OctRoute.MaxCellValue)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            var currentValue = currentBeacon.Route[j];
                            if (currentNode[currentValue] == null)
                            {
                                var newBeacon = new Beacon();
                                newBeacon.Route = currentNode.Beacon.Route.Insert(j, currentValue);
                                currentNode[currentValue] = new RouteNode(newBeacon);
                            }
                            currentNode = currentNode[currentValue];
                            if (i == j)
                            {
                                currentNode.Beacon.MacAddress = currentBeacon.MacAddress;
                            }
                        }
                        break;
                    }
                }
            }
            return result;
        }

        private static RouteNode CreateRoot(string gateWay)
        {
            var beacond = new Beacon
            {
                Route = OctRoute.Parse("FFFFFFFFFFF8"),
                MacAddress = gateWay
            };
            var result = new RouteNode(beacond);
            return result;
        }
    }
}
