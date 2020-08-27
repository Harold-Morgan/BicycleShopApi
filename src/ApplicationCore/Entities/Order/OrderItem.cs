using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(Bicycle bicycle, decimal price, bool IsLeased)
        {
            Bicycle = bicycle;
            Price = price;
            if (IsLeased)
                //Огрубляя
                LeasingEntry = new LeasingEntry();
        }

        public Order Order { get; set; }
        public Bicycle Bicycle { get; set; }
        public LeasingEntry LeasingEntry { get; set; } //Если велосипед берётся в лизинг - ставим
        public decimal Price { get; set; } //Цена велосипеда на момент покупки, в корзине, рассчитывается при покупке с учетом скидок
    }
}
