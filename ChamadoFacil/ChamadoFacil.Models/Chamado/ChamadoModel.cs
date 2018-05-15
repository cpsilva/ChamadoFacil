using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.Models.Chamado
{
    public class ChamadoModel : BaseModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public int Urgencia { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public virtual CategoriaModel Categoria { get; set; }
        public virtual UsuarioModel Usuario { get; set; }
    }
}