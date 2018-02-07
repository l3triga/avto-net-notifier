using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    public class CarPrice
    {
        public uint Price { get; set; }
        public string Currency { get; set; }

        public string Value {
            get
            {
                return ToString();
            }
        }

        public CarPrice()
        {
            Price = 0;
            Currency = "EUR";
        }

        public CarPrice(uint price)
        {
            Price = price;
            Currency = "EUR";
        }

        public CarPrice(uint price, string currency)
        {
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            return Price + " " + Currency;
        }
    }
}
