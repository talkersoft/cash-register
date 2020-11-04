using System;

namespace cashregister.Models
{
    public class Quarter : ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public Quarter()
        {
            this.Name = this.GetType().Name;
            this.Value = .25m;
            this.Currency = "USD";
        }
    }
}
