using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Authentication;
using Xunit.Abstractions;

namespace PrimaryPixelsIntegrationTest
{
    [Collection("IntegrationTests")]
    public class PhoneControllerIntegrationTest
    {
        private readonly ITestOutputHelper _output;
        private readonly PrimaryPixelsWebApplicationFactory _app;
        private readonly HttpClient _client;
        private readonly ITokenService _tokenService;

        public PhoneControllerIntegrationTest(ITestOutputHelper output)
        {
            
            _app = new PrimaryPixelsWebApplicationFactory();
            _client = _app.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task PhoneControllerIntegrationTestGetAll()
        {
            var response = await _client.GetAsync("/api/Phone");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Phone[]>();
            Assert.Equal(5, data.Length);
        }
        
        [Fact]
        public async Task PhoneControllerIntegrationTestGetById()
        {
            var response = await _client.GetAsync("/api/Phone/11");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Phone>();
            Assert.Equal("Redmi A24", data.Name);
        }
        
        [Fact]
        public async Task PhoneControllerIntegrationTestGetAllasd()
        {
            var response = await _client.GetAsync("/api/Phone/5");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}