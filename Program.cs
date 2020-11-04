using System;
using Microsoft.Extensions.Configuration;
using cashregister.Models;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using cashregister.Machine.Chamber;
using cashregister.Machine.Dispenser;

namespace cashregister
{
    class Program
    {
        private static readonly AppSettings appSettings = new AppSettings();

        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = builder.Build();
            config.GetSection("AppSettings").Bind(appSettings);

            var services = new ServiceCollection()
                .AddSingleton<CashRegisterController, CashRegisterController>()
                .AddSingleton(new CoinChamber<Penny>(100))
                .AddSingleton(new CoinChamber<Nickle>(100))
                .AddSingleton(new CoinChamber<Dime>(100))
                .AddSingleton(new CoinChamber<Quarter>(100))
                .AddSingleton(new CoinChamber<Dollar>(100))
                .AddSingleton<USDDispenser, USDDispenser>()
                .AddSingleton<USDRandomDispenser, USDRandomDispenser>()
                .AddSingleton(appSettings)
                .BuildServiceProvider();

            var controller = services.GetService<CashRegisterController>();
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
