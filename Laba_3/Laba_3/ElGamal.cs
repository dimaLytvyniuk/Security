using System;
using System.Numerics;

namespace Laba_3
{
    public static class ElGamal
    {
        private const string fileName = @"C:\Dima\Univer\Security\PrimeNumbers.txt";
        
        public static ElGamalKey GenerateKeys()
        {
            var random = new Random();
            var list = FileReader.ReadFromFileListUInt16(fileName);

            var indexOfP = random.Next(list.Count);
            UInt16 p = list[indexOfP];
            UInt16 x = (UInt16)random.Next(2, p);
            UInt16 g = PrimitiveUtilities.GetPremetiveRoot(p);
            UInt16 y = (UInt16)PrimitiveUtilities.ModularPower(g, x, p);

            return new ElGamalKey
            {
                X = x,
                Y = y,
                G = g,
                P = p
            };
        }
    }
}