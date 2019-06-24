using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNS
{
    public class Biscuit : Dish
    {
        public override int ShelfLife { get; } = 4;

        public Biscuit(string name, DateTime cooked) : base(name, cooked) { }
    }
}
