using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.WinForms.Models;

namespace Client.WinForms.Services
{
    public class StockMovementApi
    {
        private readonly HttpClient _http;
        private const string Base = "http://localhost:5238/api/stockmovements";

        public StockMovementApi(HttpClient http) => _http = http;

        public Task<List<StockMovement>> GetAllAsync() => _http.GetFromJsonAsync<List<StockMovement>>(Base);

        public async Task<StockMovement?> CreateAsync(StockMovement m)
        {
            var res = await _http.PostAsJsonAsync(Base, m);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<StockMovement>();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"{Base}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
