using System.Net;
using dotenv.net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimaryPixels.Controllers.DerivedControllers;
using PrimaryPixels.Data;
using PrimaryPixels.Exceptions;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Authentication;

namespace PrimaryPixelsIntegrationTest;

public class OrderIntegrationTest
{
    private readonly PrimaryPixelsWebApplicationFactory _app;
    private readonly HttpClient _client;
    private readonly TokenCreator _tokenCreator;
    private readonly PrimaryPixelsContext _dbContext;
    private string _userToken;
    private string _adminToken;

    public OrderIntegrationTest()
    {
        _app = new();
        _client = _app.CreateClient();
        
        DotEnv.Load();  
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables() 
            .Build();
        _tokenCreator = new(new TokenService(configuration));
        _userToken = _tokenCreator.GenerateJwtToken();
        _adminToken = _tokenCreator.GenerateJwtToken("Admin");
        _dbContext = _app.Scope.ServiceProvider.GetRequiredService<PrimaryPixelsContext>();
    }

    [Fact]
    public async Task GetOrdersWithoutAdminTokenFail()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Order");
        request.Headers.Add("Authorization", $"Bearer {_userToken}");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetOrdersWithAdminToken()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Order");
        request.Headers.Add("Authorization", $"Bearer {_adminToken}");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteOrderWithUserTokenFail()
    {
        var order = new Order
        {
            Id = 1,
            Address = "Address",
            City = "city",
            FirstName = "FirtName",
            LastName = "LastName",
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            Price = 1000,
            UserId = "userId"
        };
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Order/1");
        request.Headers.Add("Authorization", $"Bearer {_userToken}");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        var orderAfterAttempt = await _dbContext.Orders.FindAsync(1);
        Assert.NotNull(orderAfterAttempt);
    }
    
    [Fact]
    public async Task AdminCanDeleteOrderById()
    {
        var order = new Order
        {
            Id = 1,
            Address = "Address",
            City = "city",
            FirstName = "FirstName",
            LastName = "LastName",
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            Price = 1000,
            UserId = "userId"
        };
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Order/1");
        request.Headers.Add("Authorization", $"Bearer {_adminToken}");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK ,response.StatusCode);
    }

    [Fact]
    public async Task DeleteOrderByIdWithoutAnyOrderWithThatId()
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Order/1");
        request.Headers.Add("Authorization", $"Bearer {_adminToken}");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}