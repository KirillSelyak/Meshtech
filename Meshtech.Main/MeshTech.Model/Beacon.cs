namespace MeshTech.Model
{
    public class Beacon
    {
        private static Beacon invalidBeacon = new Beacon();

        public Route Route { get; set; }

        public string MacAddress { get; set; }

        public override string ToString()
        {
            var result = $"Route:{Route}, MacAddress {MacAddress}";
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
