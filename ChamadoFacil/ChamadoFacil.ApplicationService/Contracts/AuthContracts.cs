using System.ComponentModel.DataAnnotations;

namespace ChamadoFacil.ApplicationService.Contracts
{
    /// <summary>Payload for logging in with an email and password.</summary>
    public sealed record LoginRequest
    {
        [Required]
        public required string Login { get; init; }

        [Required]
        public required string Password { get; init; }
    }

    /// <summary>Payload for exchanging a refresh token for a new token pair.</summary>
    public sealed record RefreshRequest
    {
        [Required]
        public required string RefreshToken { get; init; }
    }

    /// <summary>Payload for revoking a refresh token on logout.</summary>
    public sealed record LogoutRequest
    {
        [Required]
        public required string RefreshToken { get; init; }
    }

    /// <summary>An access token and refresh token pair.</summary>
    public sealed record TokenResponse(string AccessToken, string RefreshToken);
}
