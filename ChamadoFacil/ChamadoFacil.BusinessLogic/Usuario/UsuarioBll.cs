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

        public void Cadastrar(UsuarioModel usuario)
        {
            usuario.Senha = _hash.Generate(usuario.Senha);

            _unitOfWork.CommandStack.Usuario.Adicionar(usuario);
            _unitOfWork.CommandStack.SaveChanges();
        }
    }
}