namespace ContractService.Domain.Exceptions;

public class InitializationException : Exception
{
    protected InitializationException()
    {
    }

    public InitializationException(Exception ex)
        : base("Error on initialization", ex)
    {
    }

    protected InitializationException(string message) : base(message)
    {
    }

    protected InitializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
