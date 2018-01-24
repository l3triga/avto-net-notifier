using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    class CarBrand
    {
        public string ID { get; set; }
        public string Brand { get; set; }

        public CarBrand (string id, string brand)
        {
            this.ID = id;
            this.Brand = brand;
        }
    }
}
