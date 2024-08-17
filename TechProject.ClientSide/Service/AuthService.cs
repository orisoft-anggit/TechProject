using System.Net.Http.Json;
using Blazored.LocalStorage;
using TechProject.ClientSide.Model;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<bool> Login(CreateLoginRequest loginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("login", loginRequest);
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
            if (result.Success)
            {
                await _localStorage.SetItemAsync("authToken", result.Data);
                return true;
            }
        }
        return false;
    }

    public async Task<bool> Register(CreateRegisterRequest registerRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("register", registerRequest);
        
        return response.IsSuccessStatusCode;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
    }
}