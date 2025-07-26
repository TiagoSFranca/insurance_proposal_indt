namespace ProposalService.Domain.Responses;

public class Result<T>
{
    public bool IsSuccess { get; private set; }

    public T? Value { get; private set; }

    public string[] Messages { get; private set; } = [];

    public Result()
    {
        IsSuccess = false;
    }

    public Result(T value)
    {
        Value = value;
    }

    public Result(string message)
    {
        Messages = [message];
        IsSuccess = false;
    }

    public Result(string[] messages)
    {
        Messages = messages;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new Result<T>(value);

    public static Result<T> Error() => new Result<T>();
    public static Result<T> Error(string message) => new Result<T>(message);
    public static Result<T> Error(List<string> messages) => new Result<T>(messages.ToArray());
}

