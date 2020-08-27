using ApplicationCore.Entities.Basket;
using ApplicationCore.Interfaces;
using BicycleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    class ShoppingService : IShoppingService
    {
        private IRepository _repository;

        public ShoppingService(IRepository repository)
        {
            _repository = repository;
        }


        public async Task CreateOrderAsync(ShopClient shopClient, IEnumerable<BasketItem> basketItems)
        {

            var items = Task.WhenAll(basketItems.Select(async basketItem =>
            {
                var bike = await _repository.GetByIdAsync<Bicycle>(basketItem.BicycleId);

                //Обработку цены можно и нужно вынести
                int discountsSum = (await _repository.ListAsync<Discount>())
                    .Where(x => x.Brand == bike.Brand)
                    //TODO: Вынести эту проверку валидности дискаунта
                    .Where(x => x.DiscountStart >= DateTime.Now && x.DiscountEnd <= DateTime.Now)
                    .Sum(x => x.Percentage);

                decimal newPrice = bike.Price * discountsSum;

                var orderItem = new OrderItem(bike, newPrice, basketItem.Lease);
                return orderItem;
            })).Result;

            var order = new Order(shopClient, items.ToList());


            await _repository.AddAsync(order);
        }
    }
}
