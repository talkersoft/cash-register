using System;

namespace cashregister.Models
{
    public interface ICoin
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }
    }
}
