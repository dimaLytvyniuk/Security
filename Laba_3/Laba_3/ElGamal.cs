using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

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
        
        public static ElGamalKey GenerateKeysWithDefaultP(UInt16 p = 59)
        {
            var random = new Random();
            
            UInt16 x = (UInt16)random.Next(2, p);
            UInt16 g = PrimitiveUtilities.GetRandomPremetiveRoot(p);
            UInt16 y = (UInt16)PrimitiveUtilities.ModularPower(g, x, p);

            return new ElGamalKey
            {
                X = x,
                Y = y,
                G = g,
                P = p
            };
        }

        public static KeyValuePair<UInt16, UInt16> EncryptKey(ElGamalKey key, UInt16 value)
        {
            Random random = new Random();

            var k = random.Next(1, key.P - 1);

            var a = (UInt16) PrimitiveUtilities.ModularPower(key.G, k, key.P);
            var b = (UInt16) PrimitiveUtilities.ModularPower(key.Y, k, key.P);

            var c = (b * value) % key.P;
            
            return new KeyValuePair<ushort, ushort>(a, (UInt16)c);
        }

        public static UInt16 Decrypt(ElGamalKey key, KeyValuePair<ushort, ushort> decrypted)
        {
            var apx = (UInt16) PrimitiveUtilities.ModularPower(decrypted.Key, key.P - 1 - key.X, key.P);

            var bapx = (UInt16)((decrypted.Value * apx) % key.P);

            return bapx;
        }
    }
}