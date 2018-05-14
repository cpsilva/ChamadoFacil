namespace ChamadoFacil.DataAccess
{
    public interface IUnitOfWork
    {
        ICommandStack CommandStack { get; }

        IQueryStack QueryStack { get; }
    }
}