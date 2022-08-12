using Slipstream.Application.StateNodes;

namespace Slipstream.Application.Components.DirectoryList;

public class State : BaseStateNode
{
    public IEnumerable<string> Files { get; }

    public State(string name, IEnumerable<string> files) : base(name)
        => Files = files;
    
    public new IEnumerable<IStateNode> Children => Enumerable.Empty<IStateNode>();
    
    public new void AddChild(IStateNode node)
    {
        throw new NotImplementedException();
    }

    public new void Set(IStateNode node)
    {
        throw new NotImplementedException();
    }

    public override void Accept(IStateNodeVisitor visitor)
    {
        visitor.EnterDirectoryListState(this);
        visitor.ExitDirectoryListState(this);
    }
}