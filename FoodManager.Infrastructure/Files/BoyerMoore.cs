using System;

namespace FoodManager.Infrastructure.Files
{
    public static class BoyerMoore
    {
        /// <summary>
        /// Boyer-Moore Algorithm
        /// </summary>
        public static bool Contains(this byte[] input, byte[] pattern)
        {
            int inputCurrentIndex = pattern.Length - 1;
            int patternLastIndex = pattern.Length - 1;            
            while (inputCurrentIndex < input.Length)
            {
                int indexOfLastMatch = Array.LastIndexOf(pattern, input[inputCurrentIndex]);
                if (indexOfLastMatch > -1)
                {
                    inputCurrentIndex += patternLastIndex - indexOfLastMatch;
                    for(int j = 0; j < pattern.Length; j++)
                    {
                        if(input[inputCurrentIndex - j] != pattern[patternLastIndex - j])
                        {
                            inputCurrentIndex += patternLastIndex;
                            break;
                        }

                        if(j == patternLastIndex)
                        {
                            return true;
                        }
                    }
                }
                inputCurrentIndex += patternLastIndex + 1;
            }
            return false;
        }
    }
}
