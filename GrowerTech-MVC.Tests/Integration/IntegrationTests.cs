using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using FluentAssertions;

namespace GrowerTech.Tests.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task HomePage_ShouldReturnSuccessAndCorrectContentType()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString()
                .Should().Contain("text/html");
        }

        [Fact]
        public async Task DadosClimaticos_ShouldRequireAuthentication()
        {
            // Act
            var response = await _client.GetAsync("/DadosClimaticos/Dashboard");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Redirect);
            response.Headers.Location.ToString()
                .Should().Contain("/Account/Login");
        }
    }
}