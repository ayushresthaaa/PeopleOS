namespace PeopleOS.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get;  } 
        public string? Error { get;  }

        protected Result(bool isSuccess, string? error )
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }

    }
    //we need another class for generic result, so we can return a value with the result.
    public class Result<T>: Result
    {
        public T? Value { get; }
        private Result(bool isSuccess, string? error, T? value) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> FromValue(T value)
        {
            return new Result<T>(true, null, value);
        }

        public static Result<T> FromError(string error)
        {
            return new Result<T>(false, error, default(T));
        }

        //if u inherited the `Result` class, you can use the `Success` and `Failure` methods from the base class, but they will return a `Result` object, not a `Result<T>` object. So we need to override them to return a `Result<T>` object.

    }
}