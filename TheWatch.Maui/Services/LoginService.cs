using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheWatch.Maui.Services
{
    public interface ILoginService
    {
        Task<bool> Login(string username, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        public string Email;
        public string Password; 

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(string email, string password)
        {
            // Implement the logic to make a web request for logging in
            // using the provided username and password
            // Example:
            var requestContent = new StringContent($"{{\"username\":\"{email}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://example.com/login", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
