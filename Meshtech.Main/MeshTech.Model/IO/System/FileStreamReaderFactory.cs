using System.IO;

namespace MeshTech.Model.IO.System
{
    public class FileStreamReaderFactory : IStreamReaderFactory
    {
        private readonly string path;

        public FileStreamReaderFactory(string path)
        {
            this.path = path;
        }

        public StreamReader Create()
        {
            var result = new StreamReader(path);
            return result;
        }
    }
}
