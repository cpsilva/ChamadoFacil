using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace ChamadoFacil.ApplicationService.Test
{
    [TestFixture]
    public class AuthEndpointsTests
    {
        private ApiTestFactory _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new ApiTestFactory();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task Login_WithMalformedBody_ReturnsBadRequestProblemDetails()
        {
            // Arrange
            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/login", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/problem+json"));
        }

        [Test]
        public async Task Login_WithUnknownEmail_ReturnsUnauthorized()
        {
            // Arrange
            var request = new { login = $"unknown-{Guid.NewGuid()}@example.com", password = "whatever" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task RegisterThenLogin_WithCorrectCredentials_ReturnsTokenPair()
        {
            // Arrange
            var email = $"test-{Guid.NewGuid()}@example.com";
            var registerRequest = new { nome = "Test User", email, senha = "CorrectHorse123" };
            await _client.PostAsJsonAsync("/api/usuarios", registerRequest);

            var loginRequest = new { login = email, password = "CorrectHorse123" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var body = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            Assert.That(body, Is.Not.Null);
            Assert.That(body!.AccessToken, Is.Not.Null.And.Not.Empty);
            Assert.That(body.RefreshToken, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task RegisterThenLogin_WithWrongPassword_ReturnsUnauthorized()
        {
            // Arrange
            var email = $"test-{Guid.NewGuid()}@example.com";
            var registerRequest = new { nome = "Test User", email, senha = "CorrectHorse123" };
            await _client.PostAsJsonAsync("/api/usuarios", registerRequest);

            var loginRequest = new { login = email, password = "WrongPassword" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Refresh_WithUnknownToken_ReturnsUnauthorized()
        {
            // Arrange
            var request = new { refreshToken = "not-a-real-token" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/refresh", request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task RefreshRotation_OldTokenRejectedAfterUse()
        {
            // Arrange
            var email = $"test-{Guid.NewGuid()}@example.com";
            await _client.PostAsJsonAsync("/api/usuarios", new { nome = "Test User", email, senha = "CorrectHorse123" });
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new { login = email, password = "CorrectHorse123" });
            var tokens = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDto>();

            // Act
            var firstRefresh = await _client.PostAsJsonAsync("/api/auth/refresh", new { refreshToken = tokens!.RefreshToken });
            var secondRefresh = await _client.PostAsJsonAsync("/api/auth/refresh", new { refreshToken = tokens.RefreshToken });

            // Assert
            Assert.That(firstRefresh.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(secondRefresh.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Logout_ThenRefresh_ReturnsUnauthorized()
        {
            // Arrange
            var email = $"test-{Guid.NewGuid()}@example.com";
            await _client.PostAsJsonAsync("/api/usuarios", new { nome = "Test User", email, senha = "CorrectHorse123" });
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new { login = email, password = "CorrectHorse123" });
            var tokens = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDto>();

            // Act
            var logoutResponse = await _client.PostAsJsonAsync("/api/auth/logout", new { refreshToken = tokens!.RefreshToken });
            var refreshAfterLogout = await _client.PostAsJsonAsync("/api/auth/refresh", new { refreshToken = tokens.RefreshToken });

            // Assert
            Assert.That(logoutResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(refreshAfterLogout.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        private sealed record TokenResponseDto(string AccessToken, string RefreshToken);
    }
}
