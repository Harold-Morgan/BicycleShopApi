using BicycleService.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAnalyticsService
    {
        Task<Brand> GetMostProfitableBrand(int year, int? month);

        Task<IEnumerable<ShopClient>> MostLeasingClients(int count);

        Task<IEnumerable<Discount>> GetAvaibleDiscountsForBrands(IEnumerable<Brand> brands);
        Task<Dictionary<ShopClient, Brand>> GetLastLeasedBrands(IEnumerable<ShopClient> clients);
    }
}
