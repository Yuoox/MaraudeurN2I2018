using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace MaraudeurMap.Model {
    public class CustomPin : Pin {

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Interest { get; set; }

        public string Temperature { get; set; }

        public string Country { get; set; }
        public CustomPin() {
        }
    }
}
