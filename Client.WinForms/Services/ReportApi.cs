using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.WinForms.Models;

namespace Client.WinForms.Services
{
    public class ReportApi
    {
        private readonly HttpClient _http;
        private const string Base = "api/reports";

        public ReportApi()
        {
            _http = new HttpClient { BaseAddress = new System.Uri(ApiBase.BaseUrl) };
        }

        public Task<List<OnHandDto>?> OnHandAsync() =>
            _http.GetFromJsonAsync<List<OnHandDto>>($"{Base}/onhand");

        public Task<List<LowStockDto>?> LowStockAsync(int threshold = 5) =>
            _http.GetFromJsonAsync<List<LowStockDto>>($"{Base}/lowstock?threshold={threshold}");
    }
}
