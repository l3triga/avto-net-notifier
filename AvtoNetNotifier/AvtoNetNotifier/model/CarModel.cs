using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    public class CarModel
    {
        public string Model { get; set; }

        public CarModel()
        {
            Model = "";
        }

        public CarModel(string model)
        {
            Model = model;
        }
    }
}
