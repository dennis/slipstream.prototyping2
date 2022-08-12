using Slipstream.Application.Components.DirectoryList;

namespace Slipstream.Application.StateNodes;

public interface IStateNodeVisitor
{
    public void EnterComponentRoot(ComponentRootStateNode componentRootStateNode);
    public void ExitComponentRoot(ComponentRootStateNode componentRootStateNode);
    public void EnterInstanceRoot(InstanceRootStateNode instanceRootStateNode);
    public void ExitInstanceRoot(InstanceRootStateNode instanceRootStateNode);
    public void EnterRoot(RootStateNode rootStateNode);
    public void ExitRoot(RootStateNode rootStateNode);
    
    // TODO: Make this dynamic? so this doens't need to be extended for each component
    public void EnterDirectoryListState(State state);
    public void ExitDirectoryListState(State state);
}