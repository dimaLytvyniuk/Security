using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_1
{
    public static class Generator
    {
        public static UInt16[] GenerateUInt16(int count)
        {
            var results = new UInt16[count];
            var random = new Random();

            for (int i = 0; i < count; i++)
                results[i] = (UInt16)random.Next(UInt16.MaxValue);

            return results;
        }

        public static string GetFormatedBinary16(UInt16 number)
        {
            var binary = Convert.ToString(number, 2);
            var formated = new StringBuilder(String.Format("{0, 16}", Convert.ToString(number, 2)));

            for (int i = 0; i < formated.Length - binary.Length; i++)
                formated[i] = '0';

            return formated.ToString();
        }
    }
}
