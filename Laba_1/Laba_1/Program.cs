using System;
using System.Collections.Generic;

namespace Laba_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 20000;
            string[] sequence = Generator.GenerateUInt16(count);

            double frequenceTestResult = Tester.FrequenceTest(sequence);
            double diferialTestResult = Tester.DiferialTest(sequence);
            List<Dictionary<int, double>> rangeTestResult = Tester.RangTest(sequence);
            double notLinearTestResult = Tester.TestNotLinear(sequence);

            Console.WriteLine($"Результат  частотного тесту: {frequenceTestResult}");
            Console.WriteLine($"Результат диференцiйний тесту: {String.Format("{0: 0.00000}", diferialTestResult)}");
            Console.WriteLine($"Результат тесту на нелiнiйну складнiсть: {notLinearTestResult}");

            Console.WriteLine("Результат рангового тесту:");

            for (int i = 0; i < rangeTestResult.Count; i++)
            {
                Console.WriteLine($"Вiкно {i + 2}");
                foreach (var key in rangeTestResult[i].Keys)
                {
                    Console.Write("{0}:{1,6:0.0000} ", key, rangeTestResult[i][key]);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
