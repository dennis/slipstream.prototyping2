namespace Slipstream.Application.StateNodes;

public interface IStateNode
{
    public string Name { get; }
    public IEnumerable<IStateNode> Children { get; }
    public void AddChild(IStateNode node);
    public void Set(IStateNode node);
    public void Accept(IStateNodeVisitor visitor);
}