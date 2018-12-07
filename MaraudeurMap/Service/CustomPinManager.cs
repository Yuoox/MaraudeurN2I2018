using System.Collections.Generic;
using System.Threading.Tasks;
using MaraudeurMap.Model;
using System;


namespace MaraudeurMap.Service {
    public class CustomPinManager {
        IRestService restService;

        public CustomPinManager(IRestService service) {
            restService = service;
        }

        public Task<List<CustomPin>> GetTasksAsync() {
            return restService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(CustomPin item, bool isNewItem = false) {
            return restService.SaveCustomPinAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(CustomPin item) {
            return restService.DeleteCustomPinAsync(item.Label);
        }
    }
}
