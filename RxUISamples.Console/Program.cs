using System;
using RxUISamples.ViewModels;
using System.Runtime.CompilerServices;

namespace RxUISamples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleViewModel = new SimpleViewModel();

            simpleViewModel.FirstName = "Michael";
            simpleViewModel.LastName = "Stonis";

            using(simpleViewModel.DelayChangeNotifications())
            {
                simpleViewModel.FirstName = "Miguel";
                simpleViewModel.FirstName = "Mike";
            }

            Console.WriteLine("Finishing Suppressing Change Notifications");

            Console.ReadLine();

            Console.WriteLine("Starting Delay Change Notifications");

            using (simpleViewModel.DelayChangeNotifications())
            {
                simpleViewModel.FirstName = "Michal";
                simpleViewModel.FirstName = "Thomas";
                Console.ReadLine();
            }

            Console.WriteLine("Finishing Delay Change Notifications");

            Console.ReadLine();
        }
    }
}
