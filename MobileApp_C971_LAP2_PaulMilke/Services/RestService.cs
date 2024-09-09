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
            _httpClient = new HttpClient(); 
            _serializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Term>> RefreshTermsAsync()
        {
            Terms = new List<Term>();

            //Uri uri = new Uri("http://10.0.2.2:5072/Term");

            try
            {
               HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:5072/Term");
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
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Terms; 
        }
    }
}
