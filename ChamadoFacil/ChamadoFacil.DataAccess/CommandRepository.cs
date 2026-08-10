using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseModel
    {
        private readonly DatabaseContext _dbContext;

        public CommandRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Adicionar(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Atualizar(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task ApagarAsync(int id, CancellationToken cancellationToken)
        {
            var entityTrackeable = await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
            if (entityTrackeable == null) { return; }
            _dbContext.Set<T>().Remove(entityTrackeable);
        }
    }
}
