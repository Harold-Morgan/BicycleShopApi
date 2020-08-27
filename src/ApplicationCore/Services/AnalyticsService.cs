using ApplicationCore.Interfaces;
using BicycleService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    //Получился немного "Божественным" сервисом выполняющим все задачи. Это, конечно, плохо.
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IRepository _repository;

        public AnalyticsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Brand> GetMostProfitableBrand(int year, int? month)
        {

            //Вообще, я наверное все-таки буду сторонником использования контекста здесь, но ради интереса решил использовать общий репозиторий
            //Только ради более прочного соблюдения SOA
            var filteredOrders = (await _repository.ListAsync<Order>()) 
                .Where(x => x.OrderClosedTime != null && x.OrderClosedTime.Value.Year == year); //Активные заказы

            if (month != null)
                filteredOrders = filteredOrders.Where(x => x.OrderClosedTime.Value.Month == month);

            var profitByBrand = filteredOrders
                .SelectMany(x => x.OrderItems)
                .GroupBy(x => x.Bicycle.Brand, x => x.Price, (brand, price) => new
                {
                    Brand = brand,
                    SumProfit = price.Sum()
                });

            //Есть возможность написать эффективней через Aggregate, но для демонстрации пойдёт, в принципе
            var max = profitByBrand.Max(i => i.SumProfit);
            var item = profitByBrand.First(x => x.SumProfit == max).Brand;

            return item;
        }

        public async Task<IEnumerable<ShopClient>> MostLeasingClients(int count)
        {
            //Считаем количество
            var result = (await _repository.ListAsync<OrderItem>())
                .Where(x => x.Bicycle.Leased == true)
                .GroupBy(x => x.Order.ShopClient, x => x.Bicycle, (client, leasedCount) => new
                {
                    Client = client,
                    leasedCount = leasedCount.Count()
                })
                .OrderByDescending(x => x.leasedCount)
                .Take(count)
                .Select(x => x.Client)
                .ToList()
                ;


            return result;
        }

        public async Task<IEnumerable<Discount>> GetAvaibleDiscountsForBrands(IEnumerable<Brand> brands)
        {
            return (await _repository.ListAsync<Discount>())
                .Where(x => brands.Contains(x.Brand)
                && DateTime.Now >= x.DiscountStart
                && DateTime.Now <= x.DiscountEnd
                );
        }

        public async Task<Dictionary<ShopClient,Brand>> GetLastLeasedBrands(IEnumerable<ShopClient> clients)
        {
            return (await _repository.ListAsync<LeasingEntry>())
                .Where(x => clients.Contains(x.ShopClient))
                .OrderByDescending(x => x.LeasingStart)
                .GroupBy(x => x.ShopClient)
                .Select(x => x.Last())
                .ToDictionary(x => x.ShopClient, x => x.Bicycle.Brand);
        }
    }
}
