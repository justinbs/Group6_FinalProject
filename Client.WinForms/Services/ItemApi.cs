using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.WinForms.Models;

namespace Client.WinForms.Services
{
    public class ItemApi
    {
        private readonly HttpClient _http;
        private const string Base = "http://localhost:5238/api/items";

        public ItemApi(HttpClient http) => _http = http;

        public Task<List<Item>?> GetAllAsync() => _http.GetFromJsonAsync<List<Item>>(Base);
        public Task<Item?> GetAsync(int id) => _http.GetFromJsonAsync<Item>($"{Base}/{id}");

        public async Task<Item?> CreateAsync(Item e)
        {
            var res = await _http.PostAsJsonAsync(Base, e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Item>();
        }

        public async Task<Item?> UpdateAsync(int id, Item e)
        {
            var res = await _http.PutAsJsonAsync($"{Base}/{id}", e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Item>();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"{Base}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
