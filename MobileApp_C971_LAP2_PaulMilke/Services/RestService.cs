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
        public List<Class> Classes { get; private set; }
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

        public async Task<Term> GetSingleTermAsync(int termId)
        {
            Uri uri = new Uri($"{url}/Term?TermId={termId}"); 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();
            var term = JsonSerializer.Deserialize<Term>(responseString, _serializerOptions);
            return term; 
        }

        public async Task<List<Class>> GetClassesAsync(int termId)
        {
            Classes = new List<Class>();
            Uri uri = new Uri($"{url}/Class?TermId={termId}");
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Classes = JsonSerializer.Deserialize<List<Class>>(content, _serializerOptions);
                }
                else
                {
                    Debug.WriteLine($"Failed to load terms: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Classes;
        }

        public async Task<Class> GetSingleClassAsync(int classId)
        {
            try
            {
                Uri uri = new Uri($"{url}/Class/{classId}");

                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var singleClassContent = await response.Content.ReadAsStringAsync();
                    var singleClass = JsonSerializer.Deserialize<Class>(singleClassContent, _serializerOptions);
                    return singleClass;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); 
                return null; 
            }

        }

        public async Task<bool> SaveNewClassAsync(Class newClass)
        {
            Uri uri = new Uri($"{url}/Class"); 
            string json = JsonSerializer.Serialize(newClass, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode) 
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }

        public async Task<bool> DeleteClassAsync(int classId)
        {
            Uri uri = new Uri($"{url}/Class?ClassId={classId}");

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                return false;
            }
        }
    }
}
 