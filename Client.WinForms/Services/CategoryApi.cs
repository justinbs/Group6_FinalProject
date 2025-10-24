using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.WinForms.Models;

namespace Client.WinForms.Services
{
    public class CategoryApi
    {
        private readonly HttpClient _http;
        private const string Base = "http://localhost:5238/api/categories";

        public CategoryApi(HttpClient http) => _http = http;

        public Task<List<Category>> GetAllAsync() => _http.GetFromJsonAsync<List<Category>>(Base);
        public Task<Category?> GetAsync(int id) => _http.GetFromJsonAsync<Category>($"{Base}/{id}");

        public async Task<Category?> CreateAsync(Category e)
        {
            var res = await _http.PostAsJsonAsync(Base, e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<Category?> UpdateAsync(int id, Category e)
        {
            var res = await _http.PutAsJsonAsync($"{Base}/{id}", e);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Category>();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"{Base}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
