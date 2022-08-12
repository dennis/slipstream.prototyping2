using Slipstream.Application.StateNodes;

namespace Slipstream.Application;

public interface IStateProvider
{
    public void Set(IStateNode newState);
}