using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetLibrary.Model
{
    public struct CarSummary
    {
        public uint ID { get; set; }

        public string URL { get; set; }
        public string ImageSource { get; set; }

        public string Name { get; set; }

        public string RegistrationDate { get; set; }
        public string Kilometers { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }

        public string Price { get; set; }
    }
}
