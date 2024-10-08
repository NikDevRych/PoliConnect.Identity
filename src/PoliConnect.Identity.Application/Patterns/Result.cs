namespace PoliConnect.Identity.Application.Patterns;

public class Result<TError>
{
    protected readonly TError? _error;

    public bool IsSuccess { get; }

    public TError Error => !IsSuccess
        ? _error!
        : throw new InvalidOperationException("Can not get Error if Result is Success");

    protected Result()
    {
        _error = default;
        IsSuccess = true;
    }

    protected Result(TError error)
    {
        _error = error;
        IsSuccess = false;
    }

    public static Result<TError> Success() => new();
    public static Result<TError> Failure(TError error) => new(error);
}

public sealed class Result<TValue, TError> : Result<TError>
{
    private readonly TValue? _value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Can not get Value if Result is not Success");

    private Result(TValue value) : base()
    {
        _value = value;
    }

    private Result(TError error) : base(error)
    {
        _value = default;
    }

    public static Result<TValue, TError> Success(TValue value) => new(value);
    public static new Result<TValue, TError> Failure(TError error) => new(error);
}