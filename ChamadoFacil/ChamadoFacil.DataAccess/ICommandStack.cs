using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public interface ICommandStack
    {
        ICommandRepository<Usuario> Usuario { get; }

        void SaveChanges();
    }
}