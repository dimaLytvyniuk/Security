using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_1
{
    public static class Helpers
    {
        public static List<string> BinaryStringsForBaseNumber(int bitRate)
        {
            if (bitRate <= 0)
                throw new ArgumentException();

            if (bitRate == 1)
                return new List<string> { "0", "1" };

            var maxNumber = Math.Pow(2, bitRate);

            var list = new List<string>();

            for (int i = 0; i < maxNumber; i++)
            {
                var formated = FormateNumberToBinaryString(i, bitRate);
                list.Add(formated);
            }

            return list;
        }

        public static string FormateNumberToBinaryString(int number, int bitRate)
        {
            var binary = Convert.ToString(number, 2);
            var formated = new StringBuilder(String.Format("{0, " + bitRate + "}", binary));

            for (int i = 0; i < formated.Length - binary.Length; i++)
                formated[i] = '0';

            return formated.ToString();
        }
    }
}
