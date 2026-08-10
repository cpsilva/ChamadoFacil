using ChamadoFacil.DataAccess;
using NSubstitute;

namespace ChamadoFacil.BusinessLogic.Test
{
    internal static class UnitOfWorkTestFactory
    {
        public static (IUnitOfWork UnitOfWork, IQueryStack QueryStack, ICommandStack CommandStack) Create()
        {
            var queryStack = Substitute.For<IQueryStack>();
            var commandStack = Substitute.For<ICommandStack>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.QueryStack.Returns(queryStack);
            unitOfWork.CommandStack.Returns(commandStack);

            return (unitOfWork, queryStack, commandStack);
        }
    }
}
