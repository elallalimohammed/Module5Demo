using UsersWebApi_Module3.Models;

namespace UsersWebApi_Module3.Repositories
{
    public interface IOrderRepository 
{ 
    Task<bool> CreateAsync(Order order); 
}
}