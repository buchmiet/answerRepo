namespace Trier4;

public class Answer
{
    private object _value;
    private static string _connector = " > ";

    public bool IsSuccess { get; private set; } = true;
    public bool IsTimedOut { get; private set; }
    public string Message => string.Join(_connector, Actions);
    public List<string> Actions { get; } = new List<string>();

    public static void SetConnector(string connector) => _connector = connector;

    private Answer(string action)
    {
        Actions.Add(action);
    }

    public static Answer Prepare(string action) => new Answer(action);

    public Answer WithValue(object value)
    {
        _value = value;
        return this;
    }

    public bool Out<T>(out T value)
    {
        if (_value is T t)
        {
            value = t;
            return IsSuccess;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public T GetValue<T>()
    {
        if (_value is T t)
        {
            return t;
        }
        else
        {
            throw new InvalidOperationException($"Expected type {typeof(T)}, but _value is of type {_value?.GetType()}.");
        }
    }

    public Answer Attach(Answer answer)
    {
        Actions.AddRange(answer.Actions);
        IsSuccess &= answer.IsSuccess;
        return this;
    }

    public Answer TimedOut()
    {
        IsTimedOut = true;
        IsSuccess = false;
        Actions.Add("Operation timed out.");
        return this;
    }

    public Answer Error(string message)
    {
        IsSuccess = false;
        Actions.Add(message);
        return this;
    }

    public override string ToString() => Message;
}