using System.ComponentModel.DataAnnotations;

namespace ChamadoFacil.ApplicationService.Contracts
{
    /// <summary>Payload for creating a new chamado (ticket).</summary>
    public sealed record CreateChamadoRequest
    {
        [Required, MaxLength(150)]
        public required string Titulo { get; init; }

        [Required]
        public required string Mensagem { get; init; }

        [Required]
        public required int Urgencia { get; init; }

        [Required]
        public required int CategoriaId { get; init; }
    }

    /// <summary>Payload for updating an existing chamado (ticket).</summary>
    public sealed record UpdateChamadoRequest
    {
        [Required, MaxLength(150)]
        public required string Titulo { get; init; }

        [Required]
        public required string Mensagem { get; init; }

        [Required]
        public required int Urgencia { get; init; }

        [Required]
        public required int CategoriaId { get; init; }
    }

    /// <summary>Represents a chamado (ticket) returned by the API.</summary>
    public sealed record ChamadoResponse(
        int Id,
        string Titulo,
        string Mensagem,
        int Urgencia,
        int CategoriaId,
        int UsuarioId);
}
