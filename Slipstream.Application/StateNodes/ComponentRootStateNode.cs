namespace Slipstream.Application.StateNodes;

public class ComponentRootStateNode : BaseStateNode
{
    public ComponentRootStateNode() : base("Components")
    {
        
    }
    
    public override void Accept(IStateNodeVisitor visitor)
    {
        visitor.EnterComponentRoot(this);

        foreach (var child in Children)
        {
            child.Accept(visitor);
        }
        
        visitor.ExitComponentRoot(this);
    }
}