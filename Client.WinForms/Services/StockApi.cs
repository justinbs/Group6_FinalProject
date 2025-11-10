using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.WinForms.Services
{
    public class StockApi
    {
        private readonly HttpClient _http;
        private const string Base = "api/stock";

        public StockApi()
        {
            _http = new HttpClient { BaseAddress = new System.Uri(ApiBase.BaseUrl) };
        }

        public async Task<bool> ReceiveAsync(int itemId, int quantity, string? note)
        {
            var res = await _http.PostAsJsonAsync($"{Base}/in", new { itemId, quantity, note });
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> IssueAsync(int itemId, int quantity, string? note)
        {
            var res = await _http.PostAsJsonAsync($"{Base}/out", new { itemId, quantity, note });
            return res.IsSuccessStatusCode;
        }
    }
}
