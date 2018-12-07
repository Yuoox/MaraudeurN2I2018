using MapKit;

namespace MaraudeurMap.iOS {
    public class CustomMKAnnotationView : MKAnnotationView {
        public string Id { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string City { get; set; }

        public string Interest { get; set; }

        public string Temperature { get; set; }

        public string Country { get; set;  }
        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id) {
        }
    }
}
