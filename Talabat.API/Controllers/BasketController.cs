using Microsoft.AspNetCore.Mvc;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers;
public class BasketController  : BaseApiController
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
    {
        var basket = await _basketRepository.GetCustomerBasketAsync(basketId);
        return Ok(basket ?? new CustomerBasket(basketId));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var customerBasket = await _basketRepository.UpdateCustomerBasketAsync(basket);
        return Ok(customerBasket);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteBasket(string basketId)
    {
        return await _basketRepository.DeleteCustomerBasketAsync(basketId);
    }
}
