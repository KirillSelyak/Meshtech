using System.Collections.Generic;
using MeshTech.Model;
using MeshTech.Model.IO;
using MeshTech.Model.Network;
using MeshTech.Model.Text;

namespace Meshtech.Main
{
    public static class BootStrapper
    {
        public static MeshtechTreeConstructor CreateTreeConstructor()
        {
            var result = new MeshtechTreeConstructor();
            return result;
        }

        public static IEnumerable<Beacon> CreateEnumerableBeacons(IStreamReaderFactory streamReaderFactory)
        {
            var beaconParser = new BeaconParser();
            var beaconEnumeratorFactory = new BeaconEnumeratorFactory(beaconParser);
            var result = new StreamebleBeacons(beaconEnumeratorFactory, streamReaderFactory);
            return result;
        }
    }
}
