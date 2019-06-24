using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DishNS;

namespace DishesKitchenNS
{
    public class DishesKitchen
    {
        public static DateTime TotalDate { get; set; }

        public int CountOfMenu { get; set; } = 5;

        public delegate Dish GetSomeDish(string name);

        public static Dictionary<DishesEnum, GetSomeDish> DictionOfTypes = new Dictionary<DishesEnum, GetSomeDish>
        {
            { DishesEnum.Salad, new GetSomeDish(GetSalad) },
            { DishesEnum.Cake, new GetSomeDish(GetCake) },
            { DishesEnum.Biscuit, new GetSomeDish(GetBiscuit) }
        };

        public delegate void AddDishEventHandler(object sender, AddDishEventArgs args);

        public event AddDishEventHandler AddDish;

        public async void StartCookingProcess(CancellationToken token)
        {
            while (true)
            {
                Task[] tasks = new Task[CountOfMenu * Enum.GetNames(typeof(DishesEnum)).Length];
                for (int i = 0; i < tasks.Length; i++)
                {
                    int Id = i;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Работа кухни остановлена!");
                        return;
                    }
                   
                    tasks[Id] = Task.Run(() =>
                    {
                        Thread.Sleep(1000 + Id * 50);
                        AddDish?.Invoke(this, new AddDishEventArgs
                        {
                            DishToAdd = DictionOfTypes[(DishesEnum)(Id % 3)].Invoke(Id + "_" + (int)TotalDate.DayOfWeek)
                        });
                    });
                }
                Task.WaitAll(tasks);
                TotalDate.AddDays(1);
            }
        }

        public static Dish GetSalad(string name) => new Salad("Salad_" + name, TotalDate);

        public static Dish GetCake(string name) => new Cake("Cake_" + name, TotalDate);

        public static Dish GetBiscuit(string name) => new Biscuit("Biscuit_" + name, TotalDate);
    }
}
