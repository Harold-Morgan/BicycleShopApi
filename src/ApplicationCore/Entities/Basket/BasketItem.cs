using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Basket
{
    public class BasketItem : BaseEntity
    {
        public decimal Price { get; private set; }
        public int BicycleId { get; private set; }
        //Условимся, что Lease идёт на один месяц
        public bool Lease { get; private set; }
    }
}
