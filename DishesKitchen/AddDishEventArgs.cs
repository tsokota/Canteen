using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DishNS;

namespace DishesKitchenNS
{
    public class AddDishEventArgs : EventArgs
    {
        public Dish DishToAdd { get; set; }
    }
}
