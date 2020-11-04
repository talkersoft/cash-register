using System;

namespace cashregister.Models
{
    public class Penny : ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public Penny()
        {
            this.Name = this.GetType().Name;
            this.Value = .01m;
            this.Currency = "USD";
        }
    }
}
