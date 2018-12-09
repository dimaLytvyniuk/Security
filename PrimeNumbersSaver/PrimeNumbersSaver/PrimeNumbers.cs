using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeNumbersSaver
{
    public static class PrimeNumbers
    {
        public static List<UInt16> GetAllPrimeNumbersForUInt16()
        {
            var int32List = GetAllPrimeNumbersForRange(2, UInt16.MaxValue);
            var uint16List = int32List.Select(x => (UInt16)x).ToList();

            return uint16List;
        }

        public static List<int> GetAllPrimeNumbersForRange(int minNumber = 2, int maxNumber = Int32.MaxValue)
        {
            var dictionary = FillDictionary(minNumber, maxNumber);

            var keys = dictionary.Keys.ToList();
            foreach (var key in keys)
            {
                if (dictionary[key] == true)
                {
                    for (var j = (int)Math.Pow(key, 2); j <= maxNumber; j += key)
                    {
                        dictionary[j] = false;
                    }
                } 
            }
            
            var result = dictionary
                .Where(x => x.Value == true)
                .Select(x => x.Key)
                .ToList();
            
            return result;
        }

        private static Dictionary<int, bool> FillDictionary(int min, int max)
        {
            var dictionary = new Dictionary<int, bool>();
            
            for (int i = min; i <= max; i++)
            {
                dictionary.Add(i, true);    
            }

            return dictionary;
        }
    }
}