using System.Net.Http.Json;
using Microsoft.JSInterop;
using MultRHAPI.Web.Services.Dtos;

namespace MultRHAPI.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly JwtAuthenticationStateProvider _authStateProvider;
        private const string TokenKey = "authToken";

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, JwtAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<(bool Success, string? Error)> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }

            var token = (await response.Content.ReadAsStringAsync()).Trim('"');
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
            _authStateProvider.NotifyStateChanged();

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(RegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", dto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }

            return (true, null);
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            _authStateProvider.NotifyStateChanged();
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }
    }
}
