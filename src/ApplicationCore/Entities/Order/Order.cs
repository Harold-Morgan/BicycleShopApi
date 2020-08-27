using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Order(ShopClient shopClient, List<OrderItem> items)
        {
            ShopClient = shopClient;
            OrderItems = items;
        }

        public ShopClient ShopClient { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderOpenedTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? OrderClosedTime { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
