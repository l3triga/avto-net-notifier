using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    class CarConfigurator
    {
        public List<CarBrand> CarBrands { get; }

        public CarConfigurator()
        {
            CarBrands = new List<CarBrand>();
        }
    }
}
