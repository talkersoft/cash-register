using System.Collections.Generic;
using cashregister.Models;
using cashregister.Machine.Chamber;
using System.Linq;

namespace cashregister.Machine.Dispenser
{
    public abstract class Dispenser
    {
        public Dispenser(
            CoinChamber<Penny> pennies,
            CoinChamber<Nickle> nickles,
            CoinChamber<Dime> dimes,
            CoinChamber<Quarter> quarters,
            CoinChamber<Dollar> dollars)
        {
            dispenser = new List<ICoinChamber>();
            dispenser.Add(pennies);
            dispenser.Add(nickles);
            dispenser.Add(dimes);
            dispenser.Add(quarters);
            dispenser.Add(dollars);
            dispenser = dispenser
                            .Where(x => x.Coin.Currency == currency)
                            .OrderByDescending(x => x.Coin.Value)
                            .ToList();
        }

        protected const string currency = "USD";

        protected List<ICoinChamber> dispenser;

        public CoinChamber<Dollar> Dollars
        {
            get { return (CoinChamber<Dollar>)dispenser.Where(x => x.Coin.Name == "Dollar").First(); }
        }

        public CoinChamber<Quarter> Quarters
        {
            get { return (CoinChamber<Quarter>)dispenser.Where(x => x.Coin.Name == "Quarter").First(); }
        }

        public CoinChamber<Dime> Dimes
        {
            get { return (CoinChamber<Dime>)dispenser.Where(x => x.Coin.Name == "Dime").First(); }
        }

        public CoinChamber<Nickle> Nickles
        {
            get { return (CoinChamber<Nickle>)dispenser.Where(x => x.Coin.Name == "Nickle").First(); }
        }

        public CoinChamber<Penny> Pennies
        {
            get { return (CoinChamber<Penny>)dispenser.Where(x => x.Coin.Name == "Penny").First(); }
        }

        public decimal GetTotalValue()
        {
            decimal result = 0;
            foreach(ICoinChamber c in dispenser)
            {
                result += c.TotalValue;
            }
            return result;
        }

        public virtual ChangeDue DispenseChange(decimal transactionTotal, decimal amountTendered)
        {
            var changeTray = new ChangeDue();
            changeTray.AmountDue = amountTendered - transactionTotal;

            do
                foreach(ICoinChamber c in dispenser)
                {
                    c.DispenseChange(changeTray);
                }

            while (changeTray.AmountDue > 0);

            return changeTray;
        }
    }
}