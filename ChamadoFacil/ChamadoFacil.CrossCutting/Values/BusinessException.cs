using System;

namespace ChamadoFacil.CrossCutting.Values
{
    public sealed class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}