using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class LeasingEntry : BaseEntity
    {
        public ShopClient ShopClient { get; set; }
        public Bicycle Bicycle { get; set; }
        public DateTime LeasingStart { get; set; }
        public DateTime LeasingEnd { get; set; }
    }
}
