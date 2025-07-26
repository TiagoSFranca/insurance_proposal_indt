namespace ProposalService.Domain.Responses;

public class Result<T>
{
    public bool IsSuccess { get; private set; }

    public T? Value { get; private set; }

    public string Message { get; private set; }

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
        Message = message;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new Result<T>(value);

    public static Result<T> Error() => new Result<T>();
}

