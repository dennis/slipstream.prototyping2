namespace Slipstream.Application.Components.DirectoryList;

public class Instance : IInstance
{
    private readonly FileSystemWatcher _fileSystemWatcher;
    private readonly List<string> _files = new();
    private volatile bool _changed = true;

    public Instance(Configuration configuration)
    {
        _fileSystemWatcher = new FileSystemWatcher(configuration.Path);
        
        _fileSystemWatcher.Created += OnCreated;
        _fileSystemWatcher.Deleted += OnDeleted;
        _fileSystemWatcher.Renamed += OnRenamed;
        _fileSystemWatcher.Error += OnError;
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        lock (_files)
        {
            _files.Remove(e.OldFullPath);
            _files.Add(e.FullPath);
            _changed = true;
        }
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        lock (_files)
        {
            _files.Remove(e.FullPath);
            _changed = true;
        }
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        lock (_files)
        {
            _files.Add(e.FullPath);
            _changed = true;
        }
    }

    public Task StartAsync(CancellationToken cancel = default(CancellationToken))
    {
        _fileSystemWatcher.EnableRaisingEvents = true;

        lock (_files)
        {
            _files.AddRange(Directory.GetFileSystemEntries(_fileSystemWatcher.Path));
        }
        
        return Task.CompletedTask;
    }

    public void CaptureState(IStateProvider state)
    {
        if (_changed)
        {
            lock (_files)
            {
                state.Set(new State($"DirectoryList {_fileSystemWatcher.Path}", _files));
                _changed = false;
            }
        }
    }
}