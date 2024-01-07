using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheWatch.Maui.Services
{
    public interface ISettingsService
    {
        Task<SettingsModel> GetSettingsAsync();
        Task SaveSettingsAsync(SettingsModel settings);
    }

    public class SettingsService : ISettingsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _azureFunctionUrl = "https://<your-azure-function-url>";

        public SettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_azureFunctionUrl}/getsettings");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SettingsModel>(content);
        }

        public async Task SaveSettingsAsync(SettingsModel settings)
        {
            var json = JsonSerializer.Serialize(settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_azureFunctionUrl}/savesettings", content);
            response.EnsureSuccessStatusCode();
        }
    }

    public class SettingsModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Notifications { get; set; }
        public bool LocationEnabled { get; set; }
        public bool LocationTracking { get; set; }
        public bool SMSEnabled { get; set; }
    }
}