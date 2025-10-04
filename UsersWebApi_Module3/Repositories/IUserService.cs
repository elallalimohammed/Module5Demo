using UsersWebApi_Module3.Models;

namespace UsersWebApi_Module3.Repositories
{
    public interface IUserService
    {
        Task<User> GetUserById(string userId);
    }
}