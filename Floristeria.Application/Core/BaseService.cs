namespace Floristeria.Application.Core
{
    public class BaseService : IBaseService
    {
        public ServiceResult CreateSuccessResult(string message)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message
            };
        }

        public ServiceResult CreateErrorResult(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message
            };
        }
    }
}
