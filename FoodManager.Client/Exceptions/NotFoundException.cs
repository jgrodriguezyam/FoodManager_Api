using System;

namespace FoodManager.Client.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("El registro solicitado no ha sido encontrado en el sistema.")
        {
        }
    }
}