using ChamadoFacil.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public interface IQueryRepository<T> where T : BaseModel
    {
        Task<int> ContarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<List<T>> ListarAsync(CancellationToken cancellationToken);

        Task<T> SelecionarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<T> SelecionarAsync(int id, CancellationToken cancellationToken);

        Task<T> SelecionarUltimoAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> order, CancellationToken cancellationToken);
    }
}
