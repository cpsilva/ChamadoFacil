using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public interface ICommandStack
    {
        ICommandRepository<UsuarioModel> Usuario { get; }
        ICommandRepository<ChamadoModel> Chamado { get; }
        ICommandRepository<CategoriaModel> Categoria { get; }

        void SaveChanges();
    }
}