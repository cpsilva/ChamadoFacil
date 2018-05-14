using ChamadoFacil.Models;

namespace ChamadoFacil.DataAccess
{
    public interface ICommandRepository<T> where T : BaseModel
    {
        void Apagar(int id);

        void Adicionar(T entity);

        void Atualizar(T entity);
    }
}