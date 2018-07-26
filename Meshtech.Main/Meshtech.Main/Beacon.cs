using Meshtech.Main.IO;
using System;

namespace Meshtech.Main
{
    public class Beacon : ICloneable
    {
        private static Beacon invalidBeacon = new Beacon();

        public Route Route { get; set; }
        public string MacAddress { get; set; }

        public override string ToString()
        {
            var result = $"Route:{Route}, MacAddress {MacAddress}";
            return result;
        }

        public object Clone()
        {
            var result = new Beacon();
            result.Route = (Route)Route.Clone();
            result.MacAddress = MacAddress;
            return result;
        }

        public static Beacon InvalidBeacon
        {
            get
            {
                return invalidBeacon;
            }
        }
    }
}
