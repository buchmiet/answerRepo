namespace Trier4;

public interface IAnswerService
{
    bool HasDialog { get; }
    TimeSpan Timeout { get; }
    bool HasTimeout { get; }

    Task<bool> AskAsync(string message, CancellationToken ct);
    void SetTimeout(TimeSpan timeout);
}

public class AnswerService : IAnswerService
{
    private readonly IUserDialog _dialog;
    public bool HasDialog => _dialog != null;
    public bool HasTimeout => Timeout != TimeSpan.Zero;
    public TimeSpan Timeout { get; private set; } 

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
            Console.WriteLine($"[AnswerService] Displaying dialog: {message}");
            return await _dialog.YesNoAsync(message, ct);
        }
        return false;
    }
}