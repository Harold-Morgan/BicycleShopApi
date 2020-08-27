using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Bicycle> Bicycles { get; set; }
    }
}
