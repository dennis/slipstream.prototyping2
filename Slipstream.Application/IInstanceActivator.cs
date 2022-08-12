namespace Slipstream.Application;

public interface IInstanceActivator
{
    public void Add(string name, IInstance instance);
}