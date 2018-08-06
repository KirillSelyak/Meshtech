namespace MeshTech.Model
{
    public class Beacon
    {
        private static Beacon invalidBeacon = new Beacon();
        private string macAddress = string.Empty;
        private string gateway = string.Empty;

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

        public string Gateway
        {
            get { return gateway; }
            set { gateway = value ?? string.Empty; }
        }

        public override string ToString()
        {
            var result = $"Route:{Route}, MacAddress: {MacAddress}, Gateway: {Gateway}.";
            return result;
        }

        public override bool Equals(object obj)
        {
            var beacon = obj as Beacon;
            if (beacon == null)
                return false;
            if (ReferenceEquals(this, beacon))
                return true;

            return Route.Equals(beacon.Route) && MacAddress.Equals(beacon.MacAddress) && Gateway.Equals(beacon.Gateway);
        }

        public override int GetHashCode()
        {
            var result = MacAddress.GetHashCode();
            return result;
        }
    }
}
