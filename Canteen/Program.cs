using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DishesKitchenNS;
using DishesShopNS;
using DishNS;

namespace Canteen
{
    class Program
    {
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken token = cancellationTokenSource.Token;
        private static DishesKitchen _kitchen = new DishesKitchen();
        private static DishesShop _shop = new DishesShop(_kitchen);


        static void Main(string[] args)
        {
            _shop.CountOfClients = GetInt("Введите к-во посетителей в ресторане: ");
            StartAllProcess();

            ShowMenu();

            Console.ReadLine();
        }

        public static void ShowMenu()
        {
            while(true)
            {
               switch(GetInt("\n<0> - Выйти\n<1> - Показать блюда в наличии\n"))
                {
                    case 0:
                        cancellationTokenSource.Cancel();
                        return;
                    case 1:
                        Console.Clear();
                        _shop.ShowExistense();
                        break;
                }
            }
        }

        public static async void StartAllProcess()
        {
            Task task1 = new Task(() =>
            {
                Parallel.Invoke(() => _kitchen.StartCookingProcess(token), () => _shop.StartSellingProcess(token));
            });
            task1.Start();
            Console.WriteLine("Работа кухни и ресторана запущена!");
        }

        static int GetInt(string str)
        {
            while (true)
            {
                int res;
                Console.Write(str);
                if (Int32.TryParse(Console.ReadLine(), out res))
                    return res;
            }
        }
    }
}
