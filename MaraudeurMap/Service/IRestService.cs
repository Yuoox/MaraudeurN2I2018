using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaraudeurMap.Model;

namespace MaraudeurMap.Service {
    public interface IRestService {
        Task<List<CustomPin>> RefreshDataAsync();

        Task SaveCustomPinAsync(CustomPin item, bool isNewItem);

        Task DeleteCustomPinAsync(string id);
    }
}
