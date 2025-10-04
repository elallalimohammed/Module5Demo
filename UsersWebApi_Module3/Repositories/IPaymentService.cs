using UsersWebApi_Module3.Models;

namespace UsersWebApi_Module3.Repositories
{
          public interface IPaymentService
    {
        Task<PaymentResult> ProcessPayment(Order order);
    }

    
}