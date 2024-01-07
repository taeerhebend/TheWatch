using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheWatch.Maui.Services
{
    public class SpeechRecognitionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _azureFunctionUrl = "https://<your-azure-function-url>";

        public SpeechRecognitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RecognizePhraseAsync()
        {
            // Implement speech recognition logic or call an Azure Function
            // For simplicity, this is a placeholder
            return "Recognized Phrase";
        }

        public async Task<string> GetPhraseAsync(string key)
        {
            var response = await _httpClient.GetAsync($"{_azureFunctionUrl}/getphrase?key={key}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task SavePhraseAsync(string key, string phrase)
        {
            var json = JsonSerializer.Serialize(new { Key = key, Phrase = phrase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_azureFunctionUrl}/savephrase", content);
            response.EnsureSuccessStatusCode();
        }
    }
}