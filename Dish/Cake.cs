using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNS
{
    public class Cake : Dish
    {
        public override int ShelfLife { get; } = 1;

        public Cake(string name, DateTime cooked) : base(name, cooked) { }
    }
}
