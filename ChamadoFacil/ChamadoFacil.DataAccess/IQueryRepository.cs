using ChamadoFacil.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChamadoFacil.DataAccess
{
    public interface IQueryRepository<T> where T : BaseModel
    {
        int Contar(Expression<Func<T, bool>> predicate);

        bool Existe(Expression<Func<T, bool>> predicate);

        List<T> Listar();

        T Selecionar(Expression<Func<T, bool>> predicate);

        T SelecionarUltimo<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> order);
    }
}