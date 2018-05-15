using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public class QueryStack : IQueryStack
    {
        private readonly DatabaseContext DatabaseContext;

        public IQueryRepository<UsuarioModel> Usuario { get; }

        public IQueryRepository<CategoriaModel> Categoria { get; }

        public IQueryRepository<ChamadoModel> Chamado { get; }

        public QueryStack(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;

            Usuario = new QueryRepository<UsuarioModel>(DatabaseContext);
            Categoria = new QueryRepository<CategoriaModel>(DatabaseContext);
            Chamado = new QueryRepository<ChamadoModel>(DatabaseContext);
        }
    }
}