using System;

namespace ChamadoFacil.Models.User
{
    public class Usuario : BaseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}