namespace Floristeria.Application.Core
{
    public interface IBaseService
    {
        ServiceResult CreateSuccessResult(string message);
        ServiceResult CreateErrorResult(string message);
    }
}
