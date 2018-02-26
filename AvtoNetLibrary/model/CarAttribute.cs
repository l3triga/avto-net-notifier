using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetLibrary.Model
{
    public class CarAttribute<T> : IEquatable<CarAttribute<T>>
    {
        public T Value { get; set; }
        public string Text { get; set; }

        public CarAttribute()
        {
            Value = default(T);
            Text = "";
        }

        public CarAttribute(T value)
        {
            Value = value;
            Text = "";
        }

        public CarAttribute(T value, string text)
        {
            Value = value;
            Text = text;
        }

        public bool Equals(CarAttribute<T> other)
        {
            return Value.Equals(other.Value);
        }
    }
}
