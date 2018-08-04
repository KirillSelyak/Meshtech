using System.Collections.Generic;
using System.IO;

namespace MeshTech.Model.IO
{
    public interface IBeaconEnumeratorFactory
    {
        IEnumerator<Beacon> CreateBeaconEnumerator(StreamReader streamReader);
    }
}
