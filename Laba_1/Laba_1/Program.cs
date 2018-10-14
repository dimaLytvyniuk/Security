using System;

namespace Laba_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 20000;
            string[] sequence = Generator.GenerateUInt16(count);

            for (int i = 0; i < count; i++)
                Console.WriteLine(sequence[i]);

            Console.ReadKey();
        }
    }
}
