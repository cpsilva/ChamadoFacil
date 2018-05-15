using System;
using System.Security.Cryptography;
using System.Text;

namespace ChamadoFacil.CrossCutting.Security.Hash
{
    public class Hash : IHash
    {
        public string Generate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return null; }

            using (var algorithm = new SHA512Managed())
            {
                var bytes = algorithm.ComputeHash(Encoding.Unicode.GetBytes(value));
                return Convert.ToBase64String(bytes, 0, bytes.Length);
            }
        }
    }
}