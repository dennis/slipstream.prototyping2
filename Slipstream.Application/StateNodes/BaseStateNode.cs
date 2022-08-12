namespace Slipstream.Application.StateNodes;

public abstract class BaseStateNode : IStateNode
{
    public string Name { get; }
    public IEnumerable<IStateNode> Children => _children;

    private readonly List<IStateNode> _children = new();

    public BaseStateNode(string name)
    {
        Name = name;
    }

    public void AddChild(IStateNode node)
    {
        _children.Add(node);
    }

    public void Set(IStateNode node)
    {
        _children.Clear();
        AddChild(node);
    }

    public abstract void Accept(IStateNodeVisitor visitor);

}