using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces;
public interface IBasketRepository
{
    Task<CustomerBasket> GetCustomerBasketAsync(string basketId);
    Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket basket);
    Task<bool> DeleteCustomerBasketAsync(string basketId);
}