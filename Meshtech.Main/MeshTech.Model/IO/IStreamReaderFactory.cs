using System.IO;

namespace MeshTech.Model.IO
{
    public interface IStreamReaderFactory
    {
        StreamReader Create();
    }
}
