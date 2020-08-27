using ApplicationCore.Interfaces;
using BicycleService.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleAPI.Controllers
{
    public class SendMailController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly IAnalyticsService _analyticsService;
        private readonly IEmailSender _emailSender;

        public SendMailController(ShopContext context, IAnalyticsService analyticsService, IEmailSender emailSender)
        {
            _context = context;
            _analyticsService = analyticsService;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailsToTopLeasingClients(int count)
        {
            if (count <= 0)
                return ValidationProblem();

            var topClients = await _analyticsService.MostLeasingClients(10);

            //Последние арендованные клиентами велосипеды
            //Улучшить структуру, спрятать в сервис
            var clientLastLeasedBicycles = await _analyticsService.GetLastLeasedBrands(topClients);

            var discountsOnBrands = await _analyticsService.GetAvaibleDiscountsForBrands(clientLastLeasedBicycles.Select(x => x.Value));

            var discountsOnClientBikes = clientLastLeasedBicycles.Join(discountsOnBrands,
                bike => bike.Value,
                discount => discount.Brand,
                (bike, discount) => new
                {
                    BrandName = bike.Value.Name,
                    ClientName = bike.Key.Name,
                    ClientEmail = bike.Key.Email,
                    Discount = discount.Percentage
                });


            //Очень плохая идея отсылать все и сразу на самом деле, но код здесь оставлю для демонстрации
            Task.WaitAll(discountsOnClientBikes.Select(
                discount =>
                    _emailSender.SendEmailAsync(discount.ClientEmail,
                    "Скидка на ваши любимые велосипеды!",
                    $"Привет {discount.ClientName}! \n" +
                    $"На ваш любимый велосипенд бренда {discount.BrandName}, сейчас действует скидка в {discount.Discount} процентов!")
                ).ToArray()
                );
            
            return Ok();
        }
    }
}
