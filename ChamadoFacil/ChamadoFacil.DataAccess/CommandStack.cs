using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public class CommandStack : ICommandStack
    {
        public ICommandRepository<Usuario> Usuario { get; }
        private DatabaseContext Context { get; }

        public CommandStack(DatabaseContext databaseContext)
        {
            Context = databaseContext;

            Usuario = new CommandRepository<Usuario>(Context);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}