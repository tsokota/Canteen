using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNS
{
    public abstract class Dish
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime Cooked { get;}

        public abstract int ShelfLife {get;}

        public Dish( string name,DateTime cooked)
        {
            Name = name;
            Cooked = cooked;
        }
    }
}
