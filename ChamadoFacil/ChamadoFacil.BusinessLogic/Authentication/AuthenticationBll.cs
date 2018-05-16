using ChamadoFacil.CrossCutting.Security.Hash;
using ChamadoFacil.CrossCutting.Security.JsonWebToken;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Models;
using ChamadoFacil.Models.User;
using System;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public class AuthenticationBll : IAuthenticationBll
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWT _jwt;
        private readonly IHash _hash;

        public AuthenticationBll(IUnitOfWork unitOfWork, IJWT jwt, IHash hash)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt;
            _hash = hash;
        }

        public UsuarioModel Authenticate(AuthenticationModel authentication)
        {
            authentication.Password = _hash.Generate(authentication.Password);

            var authenticated = _unitOfWork.QueryStack.Usuario.Selecionar(x => x.Email.Equals(authentication.Login) && x.Senha == authentication.Password);

            return authenticated;
        }

        public string GenerateJwt(UsuarioModel usuario)
        {
            return _jwt.Encode(usuario.Id.ToString(), null, usuario.Nome);
        }

        public void Logout(long userId)
        {
            throw new NotImplementedException();
        }
    }
}