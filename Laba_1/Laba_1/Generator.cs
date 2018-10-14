using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_1
{
    public static class Generator
    {
        public static string[] GenerateUInt16(int count)
        {
            var results = new string[count];
            var random = new Random();

            for (int i = 0; i < count; i++)
                results[i] = GetFormatedBinary16((UInt16)random.Next(UInt16.MaxValue));

            return results;
        }

        public static string GetFormatedBinary16(UInt16 number)
        {
            var binary = Convert.ToString(number, 2);
            var formated = new StringBuilder(String.Format("{0, 16}", binary));

            for (int i = 0; i < formated.Length - binary.Length; i++)
                formated[i] = '0';

            return formated.ToString();
        }
    }
}
