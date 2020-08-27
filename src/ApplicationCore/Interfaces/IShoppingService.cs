using ApplicationCore.Entities.Basket;
using BicycleService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IShoppingService
    {
        Task CreateOrderAsync(ShopClient shopClient, IEnumerable<BasketItem> basketItems);
    }
}