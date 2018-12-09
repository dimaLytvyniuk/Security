using System.Collections.Generic;
using System.IO;
using EnglishLearning.Utilities.Linq.Extensions;

namespace PrimeNumbersSaver
{
    public static class FileSaver
    {
        public static void SaveListOfInt<T>(List<T> list, string fileName, int numbersInLine = 20)
        {
            using (var writer = new StreamWriter(fileName, false))
            {
                var ranges = list.SplintOnRangesIndexes(numbersInLine);
                for (var i = 0; i < ranges.Count; i++)
                {
                    for (var j = 0; j < ranges[i].Value; j++)
                    {
                        writer.Write(list[ranges[i].Key + j].ToString());
                        writer.Write(" ");    
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}