using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    class CarBrand
    {
        public string Brand { get; set; }
        public List<CarModel> Models { get; set; }

        public CarBrand (string brand)
        {
            Brand = brand;
            Models = new List<CarModel>();
        }

        public CarBrand(string brand, List<CarModel> models)
        {
            Brand = brand;
            Models = new List<CarModel>();
        }

        public class EqualityComparer : IEqualityComparer<CarBrand>
        {
            public bool Equals(CarBrand brand1, CarBrand brand2)
            {
                return brand1.Brand.Equals(brand2.Brand);
            }

            public int GetHashCode(CarBrand brand)
            {
                return brand.Brand.GetHashCode();
            }
        }
    }
}
