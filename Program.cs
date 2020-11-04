using System;
using cashregister.Models;

namespace cashregister
{
    class Program
    {
        private static CashRegisterController controller = 
        new CashRegisterController();

        static void Main(string[] args)
        {
            //GetDispensorTotals();
            PrintChange(controller.DispenseChange(7.99m, 20.00m));

            PrintChange(controller.DispenseChange(7.00m, 10.00m));
        }

        private static void PrintChange(ChangeDue changeTray)
        {
            Console.WriteLine($"Cash Register Totals");
            Console.WriteLine($"Dollars: {changeTray.Dollars}");
            Console.WriteLine($"Quarters: {changeTray.Quarters}");
            Console.WriteLine($"Dimes: {changeTray.Dimes}");
            Console.WriteLine($"Nickles: {changeTray.Nickles}");
            Console.WriteLine($"Pennies: {changeTray.Pennies}");
        }
    }
}
