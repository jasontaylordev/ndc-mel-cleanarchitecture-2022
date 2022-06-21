using CaWorkshop.Application.Common.Models;

namespace CaWorkshop.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<(Result Result, string UserId)> CreateUserAsync(
        string userName,
        string password);

    Task<Result> DeleteUserAsync(string userId);
}
