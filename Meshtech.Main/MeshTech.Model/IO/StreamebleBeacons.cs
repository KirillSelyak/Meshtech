using System.Collections;
using System.Collections.Generic;

namespace MeshTech.Model.IO
{
    public class StreamebleBeacons : IEnumerable<Beacon>
    {
        private readonly IStreamReaderFactory streamReaderFactory;
        private readonly IBeaconEnumeratorFactory beaconEnumeratorFactory;

        public StreamebleBeacons(IBeaconEnumeratorFactory beaconEnumeratorFactory, IStreamReaderFactory streamReaderFactory)
        {
            this.beaconEnumeratorFactory = beaconEnumeratorFactory;
            this.streamReaderFactory = streamReaderFactory;
        }

        public IEnumerator<Beacon> GetEnumerator()
        {
            var streamReader = streamReaderFactory.Create();
            var result = beaconEnumeratorFactory.CreateBeaconEnumerator(streamReader);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
