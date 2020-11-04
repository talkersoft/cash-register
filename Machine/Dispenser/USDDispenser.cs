
using cashregister.Models;
using cashregister.Machine.Chamber;

namespace cashregister.Machine.Dispenser
{
    public class USDDispenser : Dispenser
    {
        public USDDispenser(
            CoinChamber<Penny> pennies,
            CoinChamber<Nickle> nickles,
            CoinChamber<Dime> dimes,
            CoinChamber<Quarter> quarters,
            CoinChamber<Dollar> dollars) : base(pennies, nickles, dimes, quarters, dollars)
        {
        }
    }
}