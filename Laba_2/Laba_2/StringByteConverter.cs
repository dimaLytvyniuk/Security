using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba_2
{
    public class StringByteConverter
    {
        public static List<UInt16> ConvertToNumbersArray(string message)
        {
            var list = message.Select(x => (UInt16) x).ToList();

            if (list.Count % 4 != 0)
            {
                for (int i = 0; i < 4 - (list.Count % 4); i++)
                    list.Add(0);
            }
            
            return list;
        }

        public static string ConvertToString(IReadOnlyCollection<UInt16> message)
        {
            var charArray = message.Select(x => (char) x).ToArray();
            
            return new string(charArray);
        }

        public static UInt64[] ConvertToUnit64Array(string message)
        {
            List<UInt16> byteArray = ConvertToNumbersArray(message);
            var byteArrayLength = byteArray.Count;

            var arrayLength = byteArrayLength % 4 != 0 ? (byteArrayLength / 4 + 1) : (byteArrayLength / 4);
            var result = new UInt64[arrayLength];

            for (int i = 0; i < byteArrayLength; i++)
            {
                var index = i / 4;
                result[index] += byteArray[i];

                if (i % 4 != 3)
                    result[index] <<= 16;
            }

            return result;
        }

        public static string ConvertToString(UInt64[] message)
        {
           var chars = new List<UInt16>();

            for (int i = 0; i < message.Length - 1; i++)
            {
                chars.Add((UInt16)((message[i] & 0xFFFF_0000_0000_0000) >> 48));
                chars.Add((UInt16)((message[i] & 0x0000_FFFF_0000_0000) >> 32));
                chars.Add((UInt16)((message[i] & 0x0000_0000_FFFF_0000) >> 16));
                chars.Add((UInt16)(message[i] & 0x0000_0000_0000_FFFF));
            }

            var last4Chars = ParseLastUInt64(message[message.Length - 1]);
            chars.AddRange(last4Chars);

            //return chars;
            return ConvertToString(chars);
        }

        private static IEnumerable<UInt16> ParseLastUInt64(UInt64 value)
        {
            var list = new List<UInt16>();
            
            list.Add((UInt16)((value & 0xFFFF_0000_0000_0000) >> 48));
            
            var lastValue = (UInt16)((value & 0x0000_FFFF_0000_0000) >> 32);
            if (lastValue == 0)
                return list;
            list.Add(lastValue);
            
            lastValue = (UInt16)((value & 0x0000_0000_FFFF_0000) >> 16);
            if (lastValue == 0)
                return list;
            list.Add(lastValue);
            
            lastValue = (UInt16)(value & 0x0000_0000_0000_FFFF);
            if (lastValue == 0)
                return list;
            list.Add(lastValue);

            return list;
        }
    }
}