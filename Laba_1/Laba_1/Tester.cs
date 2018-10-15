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
                    .Select(x => Char.GetNumericValue(x))
                    .Sum();
                frequence += frequenceInLine / sequence[i].Length;
            }

            return frequence / sequenceSize;
        }

        public static double DiferialTest(string[] sequence)
        {
            var countOfChanges = 0.0;

            foreach (var number in sequence)
            {
                var countOfChangesInNumber = 0.0;
                for (int i = 0; i < number.Length - 1; i++)
                {
                        countOfChangesInNumber += number[i] ^ number[i + 1];
                }

                countOfChanges += countOfChangesInNumber / (number.Length - 1);
            }

            return countOfChanges / sequence.Length;
        }

        public static List<Dictionary<int, double>> RangTest(string[] sequence)
        {
            var result = new List<Dictionary<int, double>>
            {
                RangeTest(sequence, 2),
                RangeTest(sequence, 3),
                RangeTest(sequence, 4),
                RangeTest(sequence, 5)
            };

            return result;
        }

        public static Dictionary<int, double> RangeTest(string[] sequence, int window)
        {
            var maxWindowValue = Math.Pow(2, window);
            var sequenceCount = sequence.Length;

            Dictionary<string, double> combinationsDictionary = Helpers.BinaryStringsForBaseNumber(window).ToDictionary(x => x, x => 0.0);

            for (int i = 0; i < sequenceCount; i++)
            {
                for (int j = 0; j < sequence[i].Length - window; j++)
                {
                    combinationsDictionary[sequence[i].Substring(j, window)] += 1;
                }
            }

            var result = new Dictionary<int, double>();
            foreach (var k in combinationsDictionary.Keys)
            {
                result[Convert.ToInt32(k, 2)] = combinationsDictionary[k] / sequenceCount;
            }
            return result;
        }

        public static double TestNotLinear(string[] sequence)
        {
            var sequenceLength = sequence.Length;
            var allLinearComplexities = new int[sequence.Length];

            for (int i = 0; i < sequenceLength; i++)
                allLinearComplexities[i] = FindNotLinearComplexityForNumber(sequence[i]);

            return allLinearComplexities.Average();
        }

        public static int FindNotLinearComplexityForNumber(string number)
        {
            for (int i = 1; i < number.Length - 2; i++)
            {
                if (CheckForNotLinearComplexity(number, i))
                    return i;
            }

            return number.Length - 1;
        }

        public static bool CheckForNotLinearComplexity(string number, int window)
        {
            var dict = new Dictionary<string, char>();

            for (int j = 0; j < number.Length - window; j++)
            {
                var subSeq = number.Substring(j, window);
                var nextValue = number[j + window];

                if (dict.TryGetValue(subSeq, out char oldNextValue))
                {
                    if (oldNextValue != nextValue)
                        return false;
                }
                else
                    dict[subSeq] = nextValue;
            }

            return true;
        }
    }
}
