using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace MaraudeurMap.Model {
    public class CustomMap : Map {

        public List<CustomPin> CustomPins { get; set; }

        public CustomMap() {

        }
    }
}
