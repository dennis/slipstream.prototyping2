using Slipstream.Application;
using Slipstream.Application.StateNodes;

public class StateProvider : IStateProvider
{
    private readonly IStateNode _rootNode;

    public StateProvider(IStateNode rootNode)
    {
        _rootNode = rootNode;
    }
    
    public void Set(IStateNode newState)
    {
        _rootNode.Set(newState);
    }
}