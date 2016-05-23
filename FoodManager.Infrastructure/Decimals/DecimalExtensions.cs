namespace FoodManager.Infrastructure.Decimals
{
    public static class DecimalExtensions
    {
        public static bool IsZero(this decimal value)
        {
            return value == 0;
        }

        public static bool IsNotZero(this decimal value)
        {
            return !IsZero(value);
        }
    }
}