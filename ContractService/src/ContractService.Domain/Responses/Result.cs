namespace ContractService.Domain.Responses;

public interface IResult
{
    bool IsSuccess { get; }
    Exception Exception { get; }
    string[] Messages { get; }
}

public class Result : IResult
{
    public bool IsSuccess { get; private set; }
    public Exception Exception { get; private set; }
    public string[] Messages { get; private set; } = [];

    private Result(bool success)
    {
        IsSuccess = success;
    }

    public Result(Exception ex)
        : this(false)
    {
        Exception = ex;
    }

    public Result(string message)
        : this(false)
    {
        Messages = [message];
    }

    public Result(string[] messages)
        : this(false)
    {
        Messages = messages;
    }

    public static Result Success() => new Result(true);

    public static Result Error() => new Result(false);
    public static Result Error(Exception ex) => new Result(ex);
    public static Result Error(string message) => new Result(message);
    public static Result Error(List<string> messages) => new Result(messages.ToArray());
}

public class Result<T> : IResult
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public Exception Exception { get; private set; }
    public string[] Messages { get; private set; } = [];

    private Result(bool success)
    {
        IsSuccess = success;
    }

    public Result(T value)
        : this(true)
    {
        Value = value;
    }

    public Result(Exception ex)
        : this(false)
    {
        Exception = ex;
    }

    public Result(string message)
        : this(false)
    {
        Messages = [message];
    }

    public Result(string[] messages)
        : this(false)
    {
        Messages = messages;
    }

    public static implicit operator Result<T>(T result) => new Result<T>(result);

    public static Result<T> Success(T value) => new Result<T>(value);

    public static Result<T> Error() => new Result<T>(false);
    public static Result<T> Error(Exception ex) => new Result<T>(ex);
    public static Result<T> Error(string message) => new Result<T>(message);
    public static Result<T> Error(List<string> messages) => new Result<T>(messages.ToArray());
}

