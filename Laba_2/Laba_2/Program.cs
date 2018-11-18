using System;
using System.Collections.Generic;

namespace Laba_2
{
    class Program
    {
        private const int KeyLength = 8;
        
        static void Main(string[] args)
        {
            string message = "Hello World!s";
            UInt32[] key = GenerateKey();
            UInt64[] substionTable = GenerateSubstitutionTable();

            IReadOnlyDictionary<int, IReadOnlyList<byte>> dictionary =
                GhostAlgorithm.BuildSubstitionTableDictionary(substionTable);
            
            UInt64[] byteArray = StringByteConverter.ConvertToUnit64Array(message);
            UInt64[] encodedArray = GhostAlgorithm.EncodeMessage(byteArray, key, substionTable);
            UInt64[] decodedArray = GhostAlgorithm.DecodeMessage(encodedArray, key, substionTable);
            
            string encodedString = StringByteConverter.ConvertToString(encodedArray);
            string decodedString = StringByteConverter.ConvertToString(decodedArray);
            string helloString = StringByteConverter.ConvertToString(byteArray);
            
            Console.WriteLine(helloString);
            Console.WriteLine(encodedString);
            Console.WriteLine(decodedString);
            Console.WriteLine(message.Length);
        }

        private static UInt32[] GenerateKey()
        {
            var random = new Random();
            var result = new UInt32[KeyLength];

            for (int i = 0; i < KeyLength; i++)
            {
                result[i] = (UInt32)random.Next(Int32.MaxValue);
            }

            return result;
        }

        private static UInt64[] GenerateSubstitutionTable()
        {
            var random = new Random();
            var result = new UInt64[KeyLength];

            for (var i = 0; i < KeyLength; i++)
            {
                var leftValue = (UInt32)(random.Next(Int32.MaxValue));
                var rightValue = (UInt32)(random.Next(Int32.MaxValue));
                result[i] = leftValue;
                result[i] <<= 32;
                result[i] |= rightValue;
            }

            return result;
        }
    }
}