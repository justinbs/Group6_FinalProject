using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.WinForms.Services
{
    internal static class ApiBase
    {
        public static string BaseUrl => Client.WinForms.ApiConfig.BaseUrl;

        public static async Task WaitForApiAsync(int timeoutMs = 15000)
        {
            using var http = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                try
                {
                    var res = await http.GetAsync("/healthz");
                    if (res.IsSuccessStatusCode) return; // API is up
                }
                catch { /* still starting */ }

                await Task.Delay(300);
            }

            throw new Exception($"API at {BaseUrl} did not respond within {timeoutMs / 1000}s.");
        }
    }
}
