namespace MeshTech.Model.Text
{
    public interface IBeaconParser
    {
        Beacon Parse(string line);
    }
}
