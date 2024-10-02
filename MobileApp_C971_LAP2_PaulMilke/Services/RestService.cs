using MobileApp_C971_LAP2_PaulMilke.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    internal class RestService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;
        

        public List<Term> Terms { get; private set; }
        private readonly string url = "https://10.0.2.2:7151";

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

        public async Task<bool> SaveNewTermAsync(Term newTerm)
        {
            Uri uri = new Uri($"{url}/Term");
            string json = JsonSerializer.Serialize(newTerm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                Debug.WriteLine($"Failed to add new term: {response.StatusCode}");
                return false; 
            }
        }

        public async Task<bool> UpdateExistingTermAsync(Term updatedTerm)
        {
            Uri uri = new Uri($"{url}/Term");
            string json = JsonSerializer.Serialize(updatedTerm);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); 

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content); 
            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                Debug.WriteLine($"Failed to update term: {response.StatusCode}");
                return false; 
            }
        }

        public async Task<bool> DeleteTermAsync(int termId)
        {
            Uri uri = new Uri($"{url}/Term?TermId={termId}");

            HttpResponseMessage response = await _httpClient.DeleteAsync(uri); 
            if (response.IsSuccessStatusCode)
            {
                return true; 
            }
            else
            {
                Debug.WriteLine($"Failed to update term: {response.StatusCode}");
                return false;
            }
        }
    }
}
