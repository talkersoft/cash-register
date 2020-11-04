
using System;
using System.Linq;
using cashregister.Models;
using cashregister.Machine.Chamber;

namespace cashregister.Machine.Dispenser
{
    public class USDRandomDispenser : Dispenser
    {
        public USDRandomDispenser(
            CoinChamber<Penny> pennies,
            CoinChamber<Nickle> nickles,
            CoinChamber<Dime> dimes,
            CoinChamber<Quarter> quarters,
            CoinChamber<Dollar> dollars) : base(pennies, nickles, dimes, quarters, dollars)
        {
        }

        public override ChangeDue DispenseChange(decimal transactionTotal, decimal amountTendered)
        {
            var changeTray = new ChangeDue();
            changeTray.AmountDue = amountTendered - transactionTotal;

            Random rnd = new Random();
            ICoinChamber[] randomChambers = dispenser.OrderBy(x => rnd.Next()).ToArray();

            do
                foreach (ICoinChamber c in randomChambers)
                {
                    c.DispenseChangeRandom(changeTray);
                }

            while (changeTray.AmountDue > 0);
            return changeTray;
        }
    }
}