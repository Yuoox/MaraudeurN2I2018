using System;
namespace MaraudeurMap.Service {
    public class RestService {
    
    HttpClient client;

        public List<CustomPin> CustomPin { get; private set; }
        var RestUrl = "";
        public RestService ()
        {
       //     var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
       //     var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient ();
            client.MaxResponseContentBufferSize = 256000;
          //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<CustomPin>> RefreshDataAsync ()
        {
            CustomPin = new List<CustomPin> ();

            var uri = new Uri (string.Format (RestUrl, string.Empty));

            try {
                var response = await client.GetAsync (uri);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync ();
                    CustomPin = JsonConvert.DeserializeObject <List<CustomPing>> (content);
                }
            } catch (Exception ex) {
                Debug.WriteLine (@"             ERROR {0}", ex.Message);
            }

            return CustomPin;
        }

        public async Task SaveCustomPinAsync (CustomPin item, bool isNewItem = false)
        {
            var uri = new Uri (string.Format (
            RestUrl, string.Empty));

            try {
                var json = JsonConvert.SerializeObject (item);
                var content = new StringContent (json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem) {
                    response = await client.PostAsync (uri, content);
                } else {
                    response = await client.PutAsync (uri, content);
                }
                
                if (response.IsSuccessStatusCode) {
                    Debug.WriteLine (@"CustomPin successfully saved.");
                }
                
            } catch (Exception ex) {
                Debug.WriteLine (@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteCustomPinAsync (string id)
        {
            var uri = new Uri (string.Format (RestUrl, id));

            try {
                var response = await client.DeleteAsync (uri);

                if (response.IsSuccessStatusCode) {
                    Debug.WriteLine (@"CustomPin successfully deleted.");   
                }
                
            } catch (Exception ex) {
                Debug.WriteLine (@"ERROR {0}", ex.Message);
            }
        

    }
}
