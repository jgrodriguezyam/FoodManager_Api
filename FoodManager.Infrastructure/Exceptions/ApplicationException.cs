using System;

namespace FoodManager.Infrastructure.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException() : base("Ocurrió un problema en la aplicación.")
        {
        }
    }
}