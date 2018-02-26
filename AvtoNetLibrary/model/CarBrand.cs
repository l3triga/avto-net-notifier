using System;
using System.Collections.Generic;
using System.Text;
using AvtoNetLibrary.Model;

namespace AvtoNetLibrary.Model
{
    public class CarBrand : CarAttribute<string>, IEquatable<CarBrand>
    {
        public List<CarAttribute<string>> Models { get; set; }

        public CarBrand() : base()
        {
            Models = new List<CarAttribute<string>>();
        }

        public CarBrand (string value) : base(value, value)
        {
            Models = new List<CarAttribute<string>>();
        }

        public CarBrand(string value, string text) : base(value, text)
        {
            Models = new List<CarAttribute<string>>();
        }

        public CarBrand(string value, List<CarAttribute<string>> models) : base(value, value)
        {
            Models = models;
        }

        public bool Equals(CarBrand other)
        {
            return Value.Equals(other.Value);
        }

        public class EqualityComparer : IEqualityComparer<CarBrand>
        {
            public bool Equals(CarBrand brand1, CarBrand brand2)
            {
                return brand1.Value.Equals(brand2.Value);
            }

            public int GetHashCode(CarBrand brand)
            {
                return brand.Value.GetHashCode();
            }
        }
    }
}
