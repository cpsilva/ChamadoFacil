using System.Threading;
using System.Threading.Tasks;
using ChamadoFacil.CrossCutting.Security.Hash;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.BusinessLogic.Usuario
{
    public class UsuarioBll : IUsuarioBll
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHash _hash;

        public UsuarioBll(IUnitOfWork unitOfWork, IHash hash)
        {
            _unitOfWork = unitOfWork;
            _hash = hash;
        }

        public async Task CadastrarAsync(UsuarioModel usuario, CancellationToken cancellationToken)
        {
            usuario.Senha = _hash.Generate(usuario.Senha);

            _unitOfWork.CommandStack.Usuario.Adicionar(usuario);
            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);
        }
    }
}
