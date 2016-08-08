using System;

namespace FoodManager.Client.Exceptions
{
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException() : base("No es posible conectar con el servidor remoto.")
        {
        }
    }

}