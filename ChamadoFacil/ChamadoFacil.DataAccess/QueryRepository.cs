using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseModel
    {
        private readonly DatabaseContext _dbContext;

        internal QueryRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> ContarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbContext
                .Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync(cancellationToken);
        }

        public Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbContext
                .Set<T>()
                .AsNoTracking()
                .AnyAsync(predicate, cancellationToken);
        }

        public Task<List<T>> ListarAsync(CancellationToken cancellationToken)
        {
            return _dbContext
                .Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<T> SelecionarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbContext
                .Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T> SelecionarAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<T>()
                .FindAsync(new object[] { id }, cancellationToken);
        }

        public Task<T> SelecionarUltimoAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> order, CancellationToken cancellationToken)
        {
            return _dbContext
                 .Set<T>()
                 .AsNoTracking()
                 .OrderByDescending(order)
                 .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
