using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    public class CarAge : IEquatable<CarAge>
    {
        public uint Age { get; set; }

        public CarAge()
        {
            Age = 0;
        }

        public CarAge(uint age)
        {
            Age = age;
        }

        public bool Equals(CarAge other)
        {
            return Age.Equals(other.Age);
        }
    }
}
