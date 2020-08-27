using ApplicationCore.Entities.Basket;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using BicycleService.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpPost]
        //Предполагается что в реальном проекте shopClient будет получаться из информации об аутентификации,
        //а не напрямую прокидываться с фронтэнда/потребителя апи
        public async Task<IActionResult> CreateOrder([FromBody] ShopClient shopClient, [FromBody] IEnumerable<BasketItem> basketItems)
        {
            if (basketItems.Count() == 0)
                return ValidationProblem();

            try
            {
                await _shoppingService.CreateOrderAsync(shopClient, basketItems);
            }
            catch (Exception ex)
            {
                //Лучше, конечно, стактрейс в логи кидать
                return Problem($"Ошибка при создании заказа: {ex.Message}");
            }

            return Ok();

        }
    }
}
