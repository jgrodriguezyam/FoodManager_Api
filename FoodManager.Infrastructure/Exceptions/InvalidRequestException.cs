using System;

namespace FoodManager.Infrastructure.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message):base(message)
        {            
        }

        public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
