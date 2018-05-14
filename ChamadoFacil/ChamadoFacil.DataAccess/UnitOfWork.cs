namespace ChamadoFacil.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICommandStack CommandStack { get; }

        public IQueryStack QueryStack { get; }

        public UnitOfWork(IQueryStack queryStack, ICommandStack commandStack)
        {
            QueryStack = queryStack;
            CommandStack = commandStack;
        }
    }
}