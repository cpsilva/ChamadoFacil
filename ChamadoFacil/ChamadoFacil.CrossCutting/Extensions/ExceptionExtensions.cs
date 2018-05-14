using System;
using System.Diagnostics;
using System.Text;

namespace ChamadoFacil.CrossCutting.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetDetail(this Exception exception)
        {
            if (exception == null) { return string.Empty; }

            var stackFrame = new StackTrace(exception, true).GetFrame(0);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"ERROR: '{exception.Message}' ");

            stringBuilder.Append($"FILE: '{stackFrame.GetMethod().DeclaringType}' ");

            stringBuilder.Append($"LINE: {stackFrame.GetFileLineNumber()}.");

            return stringBuilder.ToString();
        }
    }
}