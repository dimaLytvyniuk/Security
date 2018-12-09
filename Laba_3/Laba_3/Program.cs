using System;

namespace Laba_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var elGamalKey = ElGamal.GenerateKeys();
            
            Console.WriteLine($"p: {elGamalKey.P}");
            Console.WriteLine($"g: {elGamalKey.G}");
            Console.WriteLine($"x: {elGamalKey.X}");
            Console.WriteLine($"y: {elGamalKey.Y}");
        }
    }
}