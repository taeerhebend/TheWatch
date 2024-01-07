using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Json;


namespace TheWatch.Maui.Services
{
    public class UploadService
    {
        private readonly HttpClient _httpClient;
        private readonly string _azureFunctionUrl = "https://<your-azure-function-app>.azurewebsites.net/api/";

        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ConvertSpeechToTextAsync(Stream audioStream)
        {
            // Call Azure Function to use Cognitive Services for speech-to-text
            var response = await _httpClient.PostAsync(_azureFunctionUrl + "SpeechToText", new StreamContent(audioStream));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task UploadFileAsync(Stream fileStream, string fileName)
        {
            // Call Azure Function to upload file to Blob Storage
            var content = new StreamContent(fileStream);
            content.Headers.Add("x-filename", fileName);
            var response = await _httpClient.PostAsync(_azureFunctionUrl + "UploadFile", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
