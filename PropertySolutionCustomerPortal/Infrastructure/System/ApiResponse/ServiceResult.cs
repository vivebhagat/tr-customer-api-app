namespace PropertySolutionCustomerPortal.Infrastructure.System.ApiResponse
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Result { get; private set; }

        private ServiceResult(bool isSuccess, T result, string message)
        {
            IsSuccess = isSuccess;
            Result = result;
            Message = message;
        }

        public static ServiceResult<T> Success(T result, string message = "")
        {
            return new ServiceResult<T>(true, result, message);
        }

        public static ServiceResult<T> Failure(string message = "")
        {
            return new ServiceResult<T>(false, default, message);
        }
    }
}
