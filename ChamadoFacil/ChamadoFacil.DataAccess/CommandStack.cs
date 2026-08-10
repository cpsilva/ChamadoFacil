using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.Models.Authentication;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;
using System.Threading;
using System.Threading.Tasks;

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
            RefreshToken = new CommandRepository<RefreshTokenModel>(DatabaseContext);
        }

        public ICommandRepository<CategoriaModel> Categoria { get; }
        public ICommandRepository<ChamadoModel> Chamado { get; }
        public ICommandRepository<UsuarioModel> Usuario { get; }
        public ICommandRepository<RefreshTokenModel> RefreshToken { get; }
        private DatabaseContext DatabaseContext { get; }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DatabaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
