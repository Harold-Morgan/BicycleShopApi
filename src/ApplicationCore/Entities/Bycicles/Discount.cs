using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Entities
{
    public class Discount : BaseEntity
    {
        public Brand Brand { get; set; }
        public int Percentage { get; set; } //Пускай будет процентная скидка в int. Вообще можно поиграть-поэксперементировать
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set; }
    }
}
