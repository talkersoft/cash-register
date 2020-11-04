using System;

namespace cashregister.Models
{
    public class Nickle : ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public Nickle()
        {
            this.Name = this.GetType().Name;
            this.Value = .05m;
            this.Currency = "USD";
        }
    }
}
