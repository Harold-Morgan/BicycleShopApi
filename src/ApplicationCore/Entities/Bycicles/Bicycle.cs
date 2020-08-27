using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class Bicycle : BaseEntity
    {
        public Brand Brand { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool Leased { get; set; }

        public ICollection<Bicycle> Bicycles { get; set; }
    }
}
