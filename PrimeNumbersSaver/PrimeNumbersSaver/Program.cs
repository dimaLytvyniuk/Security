using System;
using System.Collections.Generic;

namespace PrimeNumbersSaver
{
    class Program
    {
        private const string fileName = @"C:\Dima\Univer\Security\PrimeNumbers.txt";
        
        static void Main(string[] args)
        {
            var list = PrimeNumbers.GetAllPrimeNumbersForUInt16();
            
            FileSaver.SaveListOfInt(list, fileName);
            
            Console.WriteLine("Hello World!");
        }
    }
}