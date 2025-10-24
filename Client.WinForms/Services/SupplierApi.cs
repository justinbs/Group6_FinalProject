using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.WinForms.Models;

namespace Client.WinForms.Services
{
    public class SupplierApi
    {
        private readonly HttpClient _http;
        private const string Base = "http://localhost:5238/api/suppliers";

        public SupplierApi(HttpClient http) => _http = http;

        public Task<List<Supplier>> GetAllAsync() => _http.GetFromJsonAsync<List<Supplier>>(Base);
        public Task<Supplier?> GetAsync(int id) => _http.GetFromJsonAsync<Supplier>($"{Base}/{id}");

        public async Task<Supplier?> CreateAsync(Supplier e)
        {
            var res = await _http.PostAsJsonAsync(Base, e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Supplier>();
        }

        public async Task<Supplier?> UpdateAsync(int id, Supplier e)
        {
            var res = await _http.PutAsJsonAsync($"{Base}/{id}", e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Supplier>();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"{Base}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
