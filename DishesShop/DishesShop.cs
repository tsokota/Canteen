using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DishesKitchenNS;
using DishNS;
using LogSysNS;

namespace DishesShopNS
{
    public class DishesShop
    {
        public string Name { get; set; }

        private decimal _balance = 5000;

        public int CountOfClients { get; set; } = 3;

        private Queue<Dish> _dishes;

        public DishesShop(DishesKitchen dishesKitchen)
        {
            _dishes = new Queue<Dish>();
            dishesKitchen.AddDish += AddDishToList;
        }

        public void AddDishToList(object sender, AddDishEventArgs args)
        {
            lock (_dishes)
            {
                _dishes.Enqueue(args.DishToAdd);
                LogSys.WriteToLogFile(args.DishToAdd.Name + " поступило в продажу с кухни!");
            }
        }

        public void ShowExistense()
        {
            lock (_dishes)
            {
                Console.WriteLine("В наличии: ");
                foreach (var p in _dishes)
                {
                    Console.WriteLine("   " + p.Name);
                }
            }
        }

        public async void StartSellingProcess(CancellationToken token)
        {
            while (true)
            {
                Task[] tasks = new Task[CountOfClients];
                for (int i = 0; i < tasks.Length; i++)
                {
                    int Id = i;
                    if (token.IsCancellationRequested)
                    {
                        LogSys.WriteToLogFile("Работа ресторана остановлена!");
                        return;
                    }

                    tasks[Id] = Task.Run(() =>
                    {
                        Thread.Sleep(1000 + Id * 40);
                        lock (_dishes)
                        {
                            try
                            {
                                Dish dishToSell = _dishes.Dequeue();
                                if ((DishesKitchen.TotalDate - dishToSell.Cooked).Days >= dishToSell.ShelfLife)
                                {
                                    LogSys.WriteToLogFile(dishToSell.Name + " просрочен и будет выброшен!");
                                }
                                else
                                {
                                    LogSys.WriteToLogFile(dishToSell.Name + $" продан посетителю №{Id}!");
                                }
                            }
                            catch (Exception ex)
                            {
                                LogSys.WriteToLogFile("Нету блюд к продаже!");
                            }

                        }
                    });
                }
                Task.WaitAll(tasks);
            }
        }
    }
}
