using System;

namespace cashregister.Models
{
    public class Dollar : ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public Dollar()
        {
            this.Name = this.GetType().Name;
            this.Value = 1.00m;
            this.Currency = "USD";
        }
    }
}
