namespace AuthApi.Helpers
{
    public class OperationResult<T>
    {
        public T Data { get; }
        public OperationResultStatus Status { get; }
        public string Message { get; }

        public OperationResult(OperationResultStatus status, T data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }

        public static OperationResult<T> Success(T data, string message = "")
        {
            return new OperationResult<T>(OperationResultStatus.Success, data, message);
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>(OperationResultStatus.Failure, default, message);
        }
    }

    public enum OperationResultStatus
    {
        Success,
        Failure
    }

}
