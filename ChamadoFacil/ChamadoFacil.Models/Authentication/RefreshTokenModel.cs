using ChamadoFacil.Models.User;
using System;

namespace ChamadoFacil.Models.Authentication
{
    public class RefreshTokenModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
    }
}
