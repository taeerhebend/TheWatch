using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheWatch.Maui.Services
{
    public class PasswordService
    {
        private readonly HttpClient _httpClient;
        private readonly string _azureFunctionUrl = "https://<your-azure-function-url>";

        public PasswordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ResetPassword(string password)
        {
            var hashedPassword = HashPassword(password);
            var response = await _httpClient.PostAsync($"{_azureFunctionUrl}/resetpassword", new StringContent(hashedPassword));
            response.EnsureSuccessStatusCode();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}