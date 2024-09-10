using MobileApp_C971_LAP2_PaulMilke.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    internal class RestService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;

        public List<Term> Terms { get; private set; }

        public RestService() 
        {
#if DEBUG
            HttpClientService handler = new HttpClientService();
            _httpClient = new HttpClient(handler.GetPlatformSpecificHttpClient());
#else
            _httpClient = new HttpClient(); 
#endif
            _serializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Term>> RefreshTermsAsync()
        {
            Terms = new List<Term>();
            var url = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7151" : "http://localhost:5072";
            Uri uri = new Uri($"{url}/Term");

            try
            {
               HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    string responseString = await response.Content.ReadAsStringAsync(); 
                    Terms = JsonSerializer.Deserialize<List<Term>>(responseString, _serializerOptions);
                }
                else
                {
                    Debug.WriteLine($"Failed to load terms: {response.StatusCode}");
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"\tERROR {ex}", ex.Message);
            }
            return Terms; 
        }
    }
}
