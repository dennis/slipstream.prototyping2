using Slipstream.Application.Components.DirectoryList;
using Slipstream.Application.StateNodes;

// TODO: Name state nodes
// https://docs.microsoft.com/en-us/dotnet/core/extensions/globalization-and-localization?redirectedfrom=MSDN ?

Console.WriteLine("Hello world");

var instanceContainers = new InstanceContainer();  // this is per component, right?

var stateRootNode = new RootStateNode();

var component1 = new Component(instanceContainers);
component1.CreateInstance(new Configuration("."));
var component1RootNode = new ComponentRootStateNode();
stateRootNode.AddChild(component1RootNode);

var component2 = new Component(instanceContainers);
component2.CreateInstance(new Configuration(".."));
var component2RootNode = new ComponentRootStateNode();
stateRootNode.AddChild(component2RootNode);

var instance1 = instanceContainers.Get<Instance>(".");
var instance1StateNode = new InstanceRootStateNode();
await instance1.StartAsync();
component1RootNode.AddChild(instance1StateNode);

var instance2 = instanceContainers.Get<Instance>("..");
var instance2StateNode = new InstanceRootStateNode();
await instance2.StartAsync();
component2RootNode.AddChild(instance2StateNode);

var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));

while (await periodicTimer.WaitForNextTickAsync())
{
    {
        var stateProvider = new StateProvider(instance1StateNode);
        instance1.CaptureState(stateProvider);
    }

    {
        var stateProvider = new StateProvider(instance2StateNode);
        instance2.CaptureState(stateProvider);
    }
    
    stateRootNode.Accept(new DumpVisitor());
}

public class DumpVisitor : IStateNodeVisitor
{
    private int _indent = 0;
    
    public void EnterComponentRoot(ComponentRootStateNode componentRootStateNode)
    {
        Dump(componentRootStateNode.Name, componentRootStateNode.GetType());

        _indent++;
    }

    public void ExitComponentRoot(ComponentRootStateNode componentRootStateNode)
    {
        _indent--;
    }

    public void EnterInstanceRoot(InstanceRootStateNode instanceRootStateNode)
    {
        Dump( instanceRootStateNode.Name, instanceRootStateNode.GetType());
        
        _indent++;
    }

    public void ExitInstanceRoot(InstanceRootStateNode instanceRootStateNode)
    {
        _indent--;
    }

    public void EnterRoot(RootStateNode rootStateNode)
    {
        Dump(rootStateNode.Name, rootStateNode.GetType());

        _indent++;
    }

    public void ExitRoot(RootStateNode rootStateNode)
    {
        _indent--;
    }

    public void EnterDirectoryListState(State state)
    {
        Dump(state.Name, state.GetType());

        _indent++;

        foreach (var file in state.Files)
        {
            Console.WriteLine(Indent() + " - " + file);       
        }
    }

    public void ExitDirectoryListState(State state)
    {
        _indent--;
    }
    
    public virtual void Dump(string name, Type t)
    {
        Console.WriteLine(Indent() + name + " " + t);
    }

    protected string Indent()
        => new String(' ', _indent*2);
}

