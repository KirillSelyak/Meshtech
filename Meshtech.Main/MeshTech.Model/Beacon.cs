namespace MeshTech.Model
{
    public class Beacon
    {
        private static Beacon invalidBeacon = new Beacon();
        private string macAddress = string.Empty;

        public static Beacon InvalidBeacon
        {
            get
            {
                return invalidBeacon;
            }
        }

        public OctRoute Route { get; set; }

        public string MacAddress
        {
            get { return macAddress; }
            set { macAddress = value ?? string.Empty; }
        }

        public override string ToString()
        {
            var result = $"Route:{Route}, MacAddress {MacAddress}";
            return result;
        }

        public override bool Equals(object obj)
        {
            var beacon = obj as Beacon;
            if (beacon == null)
                return false;
            if (ReferenceEquals(this, beacon))
                return true;

            return Route.Equals(beacon.Route) && MacAddress.Equals(beacon.MacAddress);
        }

        public override int GetHashCode()
        {
            var result = MacAddress.GetHashCode();
            return result;
        }
    }
}
