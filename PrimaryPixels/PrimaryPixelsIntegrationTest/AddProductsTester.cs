using System.Net;
using System.Text;
using dotenv.net;
using Microsoft.Extensions.Configuration;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Authentication;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PrimaryPixelsIntegrationTest;

[Collection("IntegrationTests")]
public class AddProductsTester
{
    private readonly ITestOutputHelper _output;
    private readonly PrimaryPixelsWebApplicationFactory _app;
    private readonly HttpClient _client;
    private readonly TokenCreator _tokenCreator;

    public AddProductsTester(ITestOutputHelper output)
    {
        _app = new PrimaryPixelsWebApplicationFactory();
        _client = _app.CreateClient();
        _output = output;
        DotEnv.Load();  

        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables() 
            .Build();
        _tokenCreator = new(new TokenService(configuration));
    }

    [Fact]
    public async Task AddNewPhoneWithCorrectDatas()
    {
        string token = _tokenCreator.GenerateJwtToken("Admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Phone");
        request.Headers.Add("Authorization", $"Bearer {token}");
        Phone phone = new()
        {
            Availability = true, CardIndependency = true, Cpu = "i3-1301", Image = "", InternalMemory = 16, Price = 100,
            Name = "Phone", Ram = 8,
        };
        var json = JsonSerializer.Serialize(phone);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        request.Content = jsonContent;
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
    
    
    [Fact]
    public async Task AddNewPhoneWithLessData()
    {
        string token = _tokenCreator.GenerateJwtToken("Admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Phone");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var jsonContent = new StringContent("Cpu:i3-6100, RAM:8", Encoding.UTF8, "application/json");
        request.Content = jsonContent;
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddProductWithUserToken()
    {
        string token = _tokenCreator.GenerateJwtToken();
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Phone");
        request.Headers.Add("Authorization", $"Bearer {token}");
        Phone phone = new()
        {
            Availability = true, CardIndependency = true, Cpu = "i3-1301", Image = "", InternalMemory = 16, Price = 100,
            Name = "Phone", Ram = 8,
        };
        var json = JsonSerializer.Serialize(phone);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        request.Content = jsonContent;
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task AddProductWithoutLogin()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Phone");
        Phone phone = new()
        {
            Availability = true, CardIndependency = true, Cpu = "i3-1301", Image = "", InternalMemory = 16, Price = 100,
            Name = "Phone", Ram = 8,
        };
        var json = JsonSerializer.Serialize(phone);
        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        request.Content = jsonContent;
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}