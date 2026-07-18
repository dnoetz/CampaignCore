using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RPG.API.IntegrationTests.Controllers;

public class AuthControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly ApiFactory _factory;
    private readonly HttpClient _client;
    
    public AuthControllerTests(ApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.ResetDatabaseAsync();
    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task Register_Then_Login_ReturnsToken()
    {
        var register = await _client.PostAsJsonAsync("api/users/register-user", new
        {
            username = "larry", firstName = "Larry", lastName = "McGee",
            password = "Password123!", email = "larry@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        register.EnsureSuccessStatusCode();

        var login = await _client.PostAsJsonAsync("api/auth/login", new
        {
            email = "larry@test.com", password = "Password123!"
        });

        login.EnsureSuccessStatusCode();
        var body = await login.Content.ReadFromJsonAsync<JsonElement>();
        Assert.False(string.IsNullOrEmpty(body.GetProperty("token").GetString()));
    }

    [Fact]
    public async Task RegisterUser_WithTakenEmail_Returns409()
    {
        var register = await _client.PostAsJsonAsync("api/users/register-user", new
        {
            username = "larry", firstName = "Larry", lastName = "McGee",
            password = "Password123!", email = "larry@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        register.EnsureSuccessStatusCode();
        
        var registerTakenEmail = await _client.PostAsJsonAsync("api/users/register-user", new
        {
            username = "alex", firstName = "Alex", lastName = "Alexander",
            password = "Password123!", email = "larry@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        Assert.Equal(HttpStatusCode.Conflict, registerTakenEmail.StatusCode);
    }
    
    [Fact]
    public async Task RegisterUser_WithTakenUsername_Returns409()
    {
        var register = await _client.PostAsJsonAsync("api/users/register-user", new
        {
            username = "larry", firstName = "Larry", lastName = "McGee",
            password = "Password123!", email = "larry@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        register.EnsureSuccessStatusCode();
        
        var registerTakenUsername = await _client.PostAsJsonAsync("api/users/register-user", new
        {
            username = "larry", firstName = "Alex", lastName = "Alexander",
            password = "Password123!", email = "alex@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        Assert.Equal(HttpStatusCode.Conflict, registerTakenUsername.StatusCode);
    }

    [Fact]
    public async Task GetCampaigns_WithoutToken_Returns401()
    {
        var response = await _client.GetAsync("api/campaigns/all-campaigns");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCampaigns_WithToken_Returns200()
    {
        var register = await _client.PostAsJsonAsync("/api/users/register-user", new
        {
            username = "larry", firstName = "Larry", lastName = "McGee",
            password = "Password123!", email = "larry@test.com", birthday = "1990-01-01T00:00:00Z"
        });
        
        register.EnsureSuccessStatusCode();
        
        var login = await _client.PostAsJsonAsync("api/auth/login", new
        {
            email = "larry@test.com", password = "Password123!"
        });

        login.EnsureSuccessStatusCode();
        var body = await login.Content.ReadFromJsonAsync<JsonElement>();
        Assert.False(string.IsNullOrEmpty(body.GetProperty("token").GetString()));
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body.GetProperty("token").GetString());
        var response = await _client.GetAsync("api/campaigns/all-campaigns");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}