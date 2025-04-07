using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Week7SupplierDataModelS00243021;

namespace Week7SupplierDataServiceS00243021
{
    public class SupplierService : ISupplierService
    {
        private HttpClient _httpClient;

        public SupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Supplier>> getSuppliers()
        {
            return await _httpClient.GetFromJsonAsync<List<Supplier>>("api/Suppliers");
        }

        public async Task<Supplier> PostSupplier(string endpoint, Supplier supplier)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, supplier);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Supplier>();
        }

        public Task<bool> login(string username, string password)
        {
            // Stub - Not used, as authentication is not required in this lab
            return Task.FromResult(true);
        }
    }
}
