using System;

namespace Laba_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var elGamalKey = ElGamal.GenerateKeysWithDefaultP(59);
            
            Console.WriteLine($"p: {elGamalKey.P}");
            Console.WriteLine($"g: {elGamalKey.G}");
            Console.WriteLine($"x: {elGamalKey.X}");
            Console.WriteLine($"y: {elGamalKey.Y}");
            
            var encrypt43 = ElGamal.EncryptKey(elGamalKey, 43);
            
            Console.WriteLine($"{encrypt43.Key} {encrypt43.Value}");

            var decrypted = ElGamal.Decrypt(elGamalKey, encrypt43);
            
            Console.WriteLine(decrypted);
        }
    }
}