using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaraudeurMap.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using MaraudeurMap.Service;
using TodoREST;

namespace MaraudeurMap {
    public partial class MainPage : ContentPage {
        CustomMap map;

        public MainPage() {
            map = new CustomMap {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(0, 0), 360, 360));

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);

            //call api
         // CustomPinManager customPinManager = new CustomPinManager(new RestService());
          //  List<CustomPin> customPins = customPinManager.GetTasksAsync();

            var pin1 = new CustomPin {
                Type = PinType.Place,
                Position = new Position(37.79752, -122.40183),
                Label = "Xamarin San Francisco Office",
                Address = "394 Pacific Ave, San Francisco CA",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9a/Gull_portrait_ca_usa.jpg",
                Interest = "Centrale nucléaire",
                Temperature = "23",
                Country = "us",
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };

            var pin2 = new CustomPin {
                Type = PinType.Place,
                Position = new Position(37.79752, -0.40183),
                Label = "Xamarin San Francisco Office",
                Address = "394 Pacific Ave, San Francisco CA",
                Temperature = "32",
                Interest = "Plateforme",
                Country = "fr",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9a/Gull_portrait_ca_usa.jpg",
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };

            map.CustomPins = new List<CustomPin> { pin1, pin2 };
            foreach (CustomPin pin in map.CustomPins) {
                map.Pins.Add(pin);
            }



            Content = stack;

        }

        public MainPage(CustomMap map) {
            this.map = map;
        }

    }

}

