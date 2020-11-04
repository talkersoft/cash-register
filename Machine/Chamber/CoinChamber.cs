using System;
using cashregister.Models;

namespace cashregister.Machine.Chamber
{
    public class CoinChamber<T>: ICoinChamber 
        where T : ICoin
    {
        public ICoin Coin { get; set; }

        private readonly Random random = new Random();

        public int Units { get; set; }

        public decimal TotalValue {
            get
            {
                return this.Coin.Value * this.Units;
            }
        }

        public CoinChamber(int units)
        {
            this.Coin = (T)Activator.CreateInstance(typeof(T));
            this.Fill(units);
        }

        private void DispenseChange(ChangeDue changeTray, int unitsToDispense)
        {
            var valueDispensed = Coin.Value * unitsToDispense;

            if (Coin.GetType() == typeof(Dollar))
            {
                changeTray.Dollars += unitsToDispense;
            }

            if (Coin.GetType() == typeof(Quarter))
            {
                changeTray.Quarters += unitsToDispense;
            }

            if (Coin.GetType() == typeof(Dime))
            {
                changeTray.Dimes += unitsToDispense;
            }

            if (Coin.GetType() == typeof(Nickle))
            {
                changeTray.Nickles += unitsToDispense;
            }

            if (Coin.GetType() == typeof(Penny))
            {
                changeTray.Pennies += unitsToDispense;
            }

            changeTray.AmountDue = changeTray.AmountDue - valueDispensed;
            this.Units -= unitsToDispense;
        }

        public void DispenseChangeRandom(ChangeDue changeTray)
        {
            var unitsToDispense = Convert.ToInt32(changeTray.AmountDue / Coin.Value);
            if (unitsToDispense > this.Units)
            {
                unitsToDispense = this.Units;
            }

            if (Coin.Value <= changeTray.AmountDue)
            {
                DispenseChange(changeTray, random.Next(1, unitsToDispense));
            }
        }

        public void DispenseChange(ChangeDue changeTray)
        {
            var unitsToDispense = Convert.ToInt32(changeTray.AmountDue / Coin.Value);
            if (unitsToDispense > this.Units)
            {
                unitsToDispense = this.Units;
            }

            if (Coin.Value <= changeTray.AmountDue)
            {
                DispenseChange(changeTray, unitsToDispense);
            }
        }

        private void Fill(int units)
        {
            if (Coin.GetType() != typeof(T))
            {
                throw new Exception("Coin not accepted in this recepticle");
            }

            this.Units += units;
        }
    }
}