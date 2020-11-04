using System;
using cashregister.Machine.Dispenser;
using cashregister.Machine.Chamber;
using cashregister.Models;

namespace cashregister
{
    public class CashRegisterController
    {
        public USDDispenser Dispenser { get; set; }

        public USDRandomDispenser RandomDispenser { get; set; }

        private CoinChamber<Penny> pennyChamber;

        private CoinChamber<Nickle> nickleChamber;

        private CoinChamber<Dime> dimeChamber;

        private CoinChamber<Quarter> quarterChamber;

        private CoinChamber<Dollar> dollarChamber;


        public CashRegisterController()
        {
            pennyChamber = new CoinChamber<Penny>(100);
            nickleChamber = new CoinChamber<Nickle>(100);
            dimeChamber = new CoinChamber<Dime>(100);
            quarterChamber = new CoinChamber<Quarter>(100);
            dollarChamber = new CoinChamber<Dollar>(100);

            this.Dispenser = new USDDispenser(
                  pennyChamber,
                  nickleChamber,
                  dimeChamber,
                  quarterChamber,
                  dollarChamber
              );

            this.RandomDispenser = new USDRandomDispenser(
                 pennyChamber,
                 nickleChamber,
                 dimeChamber,
                 quarterChamber,
                 dollarChamber
            );
        }

        public ChangeDue DispenseChange(decimal transactionTotal, decimal amountTendered)
        {
            var changeTray = new ChangeDue();
            if (amountTendered < transactionTotal)
            {
                throw new Exception("Amount Tendered must be greater than Transaction Total");
            }

            changeTray.AmountDue = amountTendered - transactionTotal;

            if (Dispenser.GetTotalValue() < changeTray.AmountDue)
            {
                throw new Exception("Amount Due is greater than amount dispensable, please fill the dispenser");
            }


            if (changeTray.AmountDue % 3 == 0)
            {
                return RandomDispenser.DispenseChange(transactionTotal, amountTendered);
            }
         
            return Dispenser.DispenseChange(transactionTotal, amountTendered);
        }
    }
}