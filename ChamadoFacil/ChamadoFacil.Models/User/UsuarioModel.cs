using ChamadoFacil.Models.Chamado;
using System.Collections.Generic;

namespace ChamadoFacil.Models.User
{
    public class UsuarioModel : BaseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<ChamadoModel> Chamados { get; set; }
    }
}