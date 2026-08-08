using Microsoft.AspNetCore.Identity;

namespace ChamadoFacil.CrossCutting.Security.Hash
{
    public class Hash : IHash
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public string Generate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return null; }

            return _passwordHasher.HashPassword(null, value);
        }

        public bool Verify(string hash, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hash) || string.IsNullOrWhiteSpace(providedPassword)) { return false; }

            var result = _passwordHasher.VerifyHashedPassword(null, hash, providedPassword);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
