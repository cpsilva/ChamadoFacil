using System.ComponentModel.DataAnnotations;

namespace ChamadoFacil.ApplicationService.Contracts
{
    /// <summary>Payload for registering a new usuario (user).</summary>
    public sealed record RegisterUsuarioRequest
    {
        [Required, MaxLength(150)]
        public required string Nome { get; init; }

        [Required, EmailAddress]
        public required string Email { get; init; }

        [Required]
        public required string Senha { get; init; }
    }
}
