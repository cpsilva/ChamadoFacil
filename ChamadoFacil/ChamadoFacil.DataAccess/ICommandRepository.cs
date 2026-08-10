using ChamadoFacil.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public interface ICommandRepository<T> where T : BaseModel
    {
        Task ApagarAsync(int id, CancellationToken cancellationToken);

        void Adicionar(T entity);

        void Atualizar(T entity);
    }
}
