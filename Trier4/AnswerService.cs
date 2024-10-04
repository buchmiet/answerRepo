namespace Trier4;

public interface IAnswerService
{
    bool HasDialog { get; }
    TimeSpan Timeout { get; }

    Task<bool> AskAsync(string message, CancellationToken ct);
    void SetTimeout(TimeSpan timeout);
}

public class AnswerService : IAnswerService
{
    private readonly IUserDialog _dialog;
    public bool HasDialog { get; private set; }
    public TimeSpan Timeout { get; private set; } = TimeSpan.FromSeconds(5);

    public AnswerService(IUserDialog dialog)
    {
        _dialog = dialog;
        HasDialog = dialog != null;
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