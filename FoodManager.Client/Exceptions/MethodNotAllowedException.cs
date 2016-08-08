using System;

namespace FoodManager.Client.Exceptions
{
    [Serializable]
    public class MethodNotAllowedException:Exception
    {
        public MethodNotAllowedException() : base("Método no permitido")
        {
            
        }
    }
}