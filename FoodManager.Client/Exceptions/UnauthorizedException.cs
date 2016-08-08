using System;

namespace FoodManager.Client.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("Acceso no autorizado.")
        {
        }
    }
}