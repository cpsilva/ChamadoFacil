using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.Categoria;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ChamadoFacil.ApplicationService.Test
{
    [TestFixture]
    public class ChamadosEndpointsTests
    {
        private ApiTestFactory _factory;
        private HttpClient _client;
        private int _categoriaId;

        [SetUp]
        public async Task SetUp()
        {
            _factory = new ApiTestFactory();
            _client = _factory.CreateClient();
            _categoriaId = await SeedCategoriaAsync();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetAll_WithoutToken_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.GetAsync("/api/chamados");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task GetById_WithUnknownId_ReturnsNotFound()
        {
            // Arrange
            await AuthenticateClientAsync();

            // Act
            var response = await _client.GetAsync("/api/chamados/999999");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task CreateGetUpdateDelete_FullLifecycle()
        {
            // Arrange
            await AuthenticateClientAsync();

            // Act — create
            var createResponse = await _client.PostAsJsonAsync("/api/chamados", new { titulo = "Teste", mensagem = "Mensagem", urgencia = 1, categoriaId = _categoriaId });

            // Assert — create
            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            var created = await createResponse.Content.ReadFromJsonAsync<ChamadoResponseDto>();
            Assert.That(created, Is.Not.Null);

            // Act — get by id
            var getResponse = await _client.GetAsync($"/api/chamados/{created!.Id}");

            // Assert — get by id
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Act — update
            var updateResponse = await _client.PutAsJsonAsync(
                $"/api/chamados/{created.Id}",
                new { titulo = "Atualizado", mensagem = "Nova mensagem", urgencia = 2, categoriaId = _categoriaId });

            // Assert — update, and no duplicate created
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var allResponse = await _client.GetAsync("/api/chamados");
            var all = await allResponse.Content.ReadFromJsonAsync<List<ChamadoResponseDto>>();
            Assert.That(all!.Count(c => c.Id == created.Id), Is.EqualTo(1));
            Assert.That(all.Single(c => c.Id == created.Id).Titulo, Is.EqualTo("Atualizado"));

            // Act — delete
            var deleteResponse = await _client.DeleteAsync($"/api/chamados/{created.Id}");

            // Assert — delete
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            var getAfterDelete = await _client.GetAsync($"/api/chamados/{created.Id}");
            Assert.That(getAfterDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_OwnsChamadoWithAuthenticatedUsersId_IgnoringClientSuppliedUsuarioId()
        {
            // Arrange
            var usuarioId = await AuthenticateClientAsync();

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/chamados",
                new { titulo = "Teste", mensagem = "Mensagem", urgencia = 1, categoriaId = _categoriaId, usuarioId = usuarioId + 999 });

            // Assert
            var created = await response.Content.ReadFromJsonAsync<ChamadoResponseDto>();
            Assert.That(created!.UsuarioId, Is.EqualTo(usuarioId));
        }

        private async Task<int> SeedCategoriaAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var categoria = new CategoriaModel { Descricao = $"Categoria-{Guid.NewGuid()}" };
            dbContext.Categorias.Add(categoria);
            await dbContext.SaveChangesAsync();
            return categoria.Id;
        }

        private async Task<int> AuthenticateClientAsync()
        {
            var email = $"test-{Guid.NewGuid()}@example.com";
            await _client.PostAsJsonAsync("/api/usuarios", new { nome = "Test User", email, senha = "CorrectHorse123" });
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new { login = email, password = "CorrectHorse123" });
            var tokens = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDto>();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens!.AccessToken);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            return dbContext.Usuarios.Single(u => u.Email == email).Id;
        }

        private sealed record TokenResponseDto(string AccessToken, string RefreshToken);
        private sealed record ChamadoResponseDto(int Id, string Titulo, string Mensagem, int Urgencia, int CategoriaId, int UsuarioId);
    }
}
