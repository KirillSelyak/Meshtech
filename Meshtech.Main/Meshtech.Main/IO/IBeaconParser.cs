namespace Meshtech.Main.IO
{
    public interface IBeaconParser
    {
        Beacon Parse(string line);
    }
}
