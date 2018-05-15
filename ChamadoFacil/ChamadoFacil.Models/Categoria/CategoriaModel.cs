using ChamadoFacil.Models.Chamado;
using System.Collections.Generic;

namespace ChamadoFacil.Models.Categoria
{
    public class CategoriaModel : BaseModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<ChamadoModel> Chamados { get; set; }
    }
}