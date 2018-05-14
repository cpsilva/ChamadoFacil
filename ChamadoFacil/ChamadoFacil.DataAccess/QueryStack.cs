using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public class QueryStack : IQueryStack
    {
        private readonly DatabaseContext context;

        public IQueryRepository<Usuario> Usuario { get; }

        public QueryStack(DatabaseContext databaseContext)
        {
            context = databaseContext;

            Usuario = new QueryRepository<Usuario>(context);
        }
    }
}