using System;

namespace cashregister.Models
{
    public class Dime : ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public Dime()
        {
            this.Name = this.GetType().Name;
            this.Value = .10m;
            this.Currency = "USD";
        }
    }
}
