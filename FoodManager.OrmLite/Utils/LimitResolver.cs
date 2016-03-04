namespace FoodManager.OrmLite.Utils
{
    public static class LimitResolver
    {
        private const int NumberOfRowsToSubtract = 1;
        private const int NumberOfRowsToSum = 1;

        public static int ConvertSkip(this int startRow)
        {
            return (startRow - NumberOfRowsToSubtract);
        }

        public static int ConvertRows(this int startRow, int endRow)
        {
            return ((endRow - startRow) + NumberOfRowsToSum);
        }
    }
}