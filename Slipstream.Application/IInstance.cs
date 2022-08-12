namespace Slipstream.Application;

public interface IInstance
{
    public Task StartAsync(CancellationToken cancel = default(CancellationToken));
    public void CaptureState(IStateProvider state);
}