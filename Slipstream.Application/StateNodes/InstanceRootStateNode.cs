namespace Slipstream.Application.StateNodes;

public class InstanceRootStateNode : BaseStateNode
{
    public InstanceRootStateNode() : base("Instances")
    {
    }
    
    public override void Accept(IStateNodeVisitor visitor)
    {
        visitor.EnterInstanceRoot(this);

        foreach (var child in Children)
        {
            child.Accept(visitor);
        }
        
        visitor.ExitInstanceRoot(this);
    }
}