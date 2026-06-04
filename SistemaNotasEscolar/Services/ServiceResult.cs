namespace SistemaNotasEscolar.Services;

public class ServiceResult
{
    protected ServiceResult(bool success, string? errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public bool Success { get; }

    public string? ErrorMessage { get; }

    public static ServiceResult Ok()
    {
        return new ServiceResult(true, null);
    }

    public static ServiceResult Fail(string errorMessage)
    {
        return new ServiceResult(false, errorMessage);
    }
}

public class ServiceResult<T> : ServiceResult
{
    private ServiceResult(bool success, string? errorMessage, T? value)
        : base(success, errorMessage)
    {
        Value = value;
    }

    public T? Value { get; }

    public static ServiceResult<T> Ok(T value)
    {
        return new ServiceResult<T>(true, null, value);
    }

    public new static ServiceResult<T> Fail(string errorMessage)
    {
        return new ServiceResult<T>(false, errorMessage, default);
    }
}
