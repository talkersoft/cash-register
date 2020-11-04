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

            var lines = System.IO.File.ReadAllLines("transaction-input.csv");
            foreach (string line in lines)
            {
                try
                {
                    var values = line.Split(",");
                    decimal amountOwed = decimal.Parse(values[0]);
                    decimal amountPaid = decimal.Parse(values[1]);
                    PrintChange(controller.DispenseChange(amountOwed, amountPaid));
                }
                catch (Exception ex)
                {
                    using (StreamWriter file = new StreamWriter(@"transaction-output.csv", true))
                    {
                        var output = ex.Message;
                        Console.WriteLine(output);
                        file.WriteLine(output);
                    }
                }
            }
        }

        private static void PrintChange(ChangeDue changeTray)
        {
            using (StreamWriter file = new StreamWriter(@"transaction-output.csv", true))
            {
                var output = $"{changeTray.Dollars} dollar, {changeTray.Quarters} quarters, {changeTray.Dimes} dimes, {changeTray.Nickles} nickles, {changeTray.Pennies} pennies";
                Console.WriteLine(output);
                file.WriteLine(output);
            }
        }
    }
}
