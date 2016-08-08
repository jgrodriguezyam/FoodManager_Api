using System;

namespace FoodManager.Client.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("La solicitud es inválida.")
        {
        }
    }
}