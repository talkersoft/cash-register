using System;
using cashregister.Machine.Dispenser;
using cashregister.Machine.Chamber;
using cashregister.Models;
using Microsoft.Extensions.Configuration;

namespace cashregister
{
    public class CashRegisterController
    {
        private USDDispenser dispenser { get; set; }

        private USDRandomDispenser randomDispenser { get; set; }

        private AppSettings appSettings;

        public CashRegisterController(AppSettings appSettings,
                                      USDDispenser usdDispenser,
                                      USDRandomDispenser usdRandomDispenser)
        {
            this.dispenser = usdDispenser;
            this.randomDispenser = usdRandomDispenser;
            this.appSettings = appSettings;
        }

        private bool MinimumChangeAvailable()
        {
            return dispenser.Pennies.Units >= 5 && dispenser.Nickles.Units >= 1 && dispenser.Dimes.Units >= 2 && dispenser.Quarters.Units >= 3 && dispenser.Dollars.Units >= 19;
        }

        public ChangeDue DispenseChange(decimal transactionTotal, decimal amountTendered)
        {
            var changeTray = new ChangeDue();
            if (amountTendered < transactionTotal)
            {
                throw new Exception("Amount Tendered must be greater than Transaction Total");
            }

            changeTray.AmountDue = amountTendered - transactionTotal;

            if (dispenser.GetTotalValue() < changeTray.AmountDue && MinimumChangeAvailable())
            {
                throw new Exception("Amount Due is greater than amount dispensable, please fill the dispenser");
            }

            if (changeTray.AmountDue % appSettings.Divisor == 0)
            {
                return randomDispenser.DispenseChange(transactionTotal, amountTendered);
            }
         
            return dispenser.DispenseChange(transactionTotal, amountTendered);
        }
    }
}