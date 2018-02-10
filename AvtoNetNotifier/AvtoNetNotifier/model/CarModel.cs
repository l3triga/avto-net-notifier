using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetNotifier
{
    public class CarModel : IEquatable<CarModel>
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

        public bool Equals(CarModel other)
        {
            return this.Model.Equals(other.Model);
        }
    }
}
