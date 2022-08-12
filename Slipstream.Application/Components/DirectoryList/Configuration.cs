namespace Slipstream.Application.Components.DirectoryList;

public class Configuration
{
    public string Path { get; }

    public Configuration(string path)
        => Path = path;
}