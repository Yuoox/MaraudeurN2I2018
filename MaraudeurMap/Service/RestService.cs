using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MaraudeurMap.Model;
using MaraudeurMap.Service;
using Newtonsoft.Json;

namespace TodoREST {
    public class RestService : IRestService {
        HttpClient client;

        public List<CustomPin> Items { get; private set; }
        string RestUrl = "https://opendata-nuit.herokuapp.com/api/meteos";
        public RestService() {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<CustomPin>> RefreshDataAsync() {
            Items = new List<CustomPin>();

            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<CustomPin>>(content);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveCustomPinAsync(CustomPin item, bool isNewItem = false) {
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem) {
                    response = await client.PostAsync(uri, content);
                }
                else {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode) {
                    Debug.WriteLine(@"CustomPin successfully saved.");
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteCustomPinAsync(string id) {
            var uri = new Uri(string.Format(RestUrl, id));

            try {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode) {
                    Debug.WriteLine(@"CustomPin successfully deleted.");
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(@" ERROR {0}", ex.Message);
            }
        }

    }
}
