namespace MeshTech.Model.IO
{
    public interface IBeaconParser
    {
        Beacon Parse(string line);
    }
}
