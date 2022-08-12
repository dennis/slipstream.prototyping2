using System.Reflection.PortableExecutable;

namespace Slipstream.Application.StateNodes;

public class RootStateNode : BaseStateNode
{
    public RootStateNode() : base("ROOT")
    {
    }
    
    public override void Accept(IStateNodeVisitor visitor)
    {
        visitor.EnterRoot(this);

        foreach (var child in Children)
        {
            child.Accept(visitor);
        }
        
        visitor.ExitRoot(this);
    }
}