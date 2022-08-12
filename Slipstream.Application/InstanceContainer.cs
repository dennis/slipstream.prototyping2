using Slipstream.Application;

public class InstanceContainer : IInstanceActivator
{
    private readonly Dictionary<string, IInstance> _instances = new();
    
    public void Add(string name, IInstance instance)
    {
        _instances.Add(name, instance);
    }

    public T Get<T>(string name)
        => (T) _instances[name];
}