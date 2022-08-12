namespace Slipstream.Application.Components.DirectoryList;

public class Component : IComponent
{
    private readonly IInstanceActivator _instanceActivator;

    public Component(IInstanceActivator instanceActivator)
    {
        _instanceActivator = instanceActivator;
    }
    
    public void CreateInstance(Configuration configuration)
    {
        var instance = new Instance(configuration);
        
        _instanceActivator.Add(configuration.Path, instance);
    }
}