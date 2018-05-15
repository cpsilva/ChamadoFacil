using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public class CommandStack : ICommandStack
    {
        public CommandStack(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;

            Usuario = new CommandRepository<UsuarioModel>(DatabaseContext);
            Chamado = new CommandRepository<ChamadoModel>(DatabaseContext);
            Categoria = new CommandRepository<CategoriaModel>(DatabaseContext);
        }

        public ICommandRepository<CategoriaModel> Categoria { get; }
        public ICommandRepository<ChamadoModel> Chamado { get; }
        public ICommandRepository<UsuarioModel> Usuario { get; }
        private DatabaseContext DatabaseContext { get; }

        public void SaveChanges()
        {
            DatabaseContext.SaveChanges();
        }
    }
}