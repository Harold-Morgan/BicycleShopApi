using ApplicationCore.Interfaces;
using BicycleService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BicycleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<ActionResult<Brand>> GetMostProfitableBrand(int year, int? month)
        {
            if (year < 0 || month < 0)
                return ValidationProblem();

            return await _analyticsService.GetMostProfitableBrand(year, month);

        }

        [HttpGet]
        public async Task<IEnumerable<ShopClient>> GetTopLeasingClients(int count)
        {
            return await _analyticsService.MostLeasingClients(count);
        }


    }
}
