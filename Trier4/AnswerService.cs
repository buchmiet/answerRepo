namespace Trier4;

public interface IAnswerService
{
    bool HasDialog { get; }
    TimeSpan Timeout { get; }
    bool HasTimeout { get; }

    void AddYesNoDialog(IUserDialog dialog);
    Task<bool> AskYesNoAsync(string message, CancellationToken ct);
    Task<bool> AskYesNoToWaitAsync(string message, CancellationToken ct);
    void SetTimeout(TimeSpan timeout);
}

public class AnswerService : IAnswerService
{
    private IUserDialog _dialog;
    public bool HasDialog => _dialog != null;
    public bool HasTimeout => Timeout != TimeSpan.Zero;
    public TimeSpan Timeout { get; private set; } 

    public void AddYesNoDialog(IUserDialog dialog)
    {
        _dialog = dialog;
    }

    public Task<bool> AskYesNoAsync(string message, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AskYesNoToWaitAsync(string message, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public AnswerService(IUserDialog dialog)
    {
        _dialog = dialog;
    }

    public AnswerService()
    {
    }


    public void SetTimeout(TimeSpan timeout)
    {
        Timeout = timeout;
    }

    

    public async Task<bool> AskAsync(string message, CancellationToken ct)
    {
        if (HasDialog)
        {
            Console.WriteLine($"[AnswerService] Displaying dialog:");
            return await _dialog.YesNoAsync(message, ct);
        }
        return false;
    }
}