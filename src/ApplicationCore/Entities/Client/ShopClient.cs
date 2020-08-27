using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class ShopClient : BaseEntity
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Bicycle> Clients { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
