using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Laba_1
{
    public static class Tester
    {
        public static double FrequenceTest(string[] sequence)
        {
            var frequence = 0.0;

            int sequenceSize = sequence.Length;
            for (int i = 0; i < sequenceSize; i++)
            {
                var frequenceInLine = sequence[i]
                    .Select(x => Convert.ToInt32(x))
                    .Sum();
                frequence += frequenceInLine / sequence[i].Length;
            }

            return frequence / sequenceSize;
        }

        public static double DiferialTest(string[] sequence)
        {
            var countOfChanges = 0;

            foreach (var number in sequence)
            {
                for (int i = 0; i < number.Length - 1; i++)
                {
                    if (number[i] != number[i + i])
                        countOfChanges += 1;
                }
            }

            return countOfChanges / sequence.Length;
        }

        public static List<Dictionary<int, int>> RangTest(string[] sequence)
        {
            var result = new List<Dictionary<int, int>>
            {
                RangeTest(sequence, 2),
                RangeTest(sequence, 3),
                RangeTest(sequence, 4),
                RangeTest(sequence, 5)
            };

            return result;
        }

        public static Dictionary<int, int> RangeTest(string[] sequence, int window)
        {
            var maxWindowValue = Math.Pow(2, window);
            var sequenceCount = sequence.Length;

            Dictionary<string, int> combinationsDictionary = Helpers.BinaryStringsForBaseNumber(window).ToDictionary(x => x, x => 0);

            for (int i = 0; i < sequenceCount; i++)
            {
                for (int j = 0; j < sequence[i].Length - window; j++)
                {
                    combinationsDictionary[sequence[j].Substring(j, window)] += 1;
                }
            }

            var result = new Dictionary<int, int>();
            foreach (var k in combinationsDictionary.Keys)
            {
                result[Convert.ToInt32(k)] = combinationsDictionary[k] / sequenceCount;
            }
            return result;
        }

        public static int TestNotLinear(string[] sequence)
        {
            return 0;
        }
    }
}
