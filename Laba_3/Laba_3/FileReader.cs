using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laba_3
{
    public static class FileReader
    {
        public static List<UInt16> ReadFromFileListUInt16(string fileName)
        {
            var result = new List<UInt16>();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                { 
                    var numbers = reader.ReadLine()
                        .Split(' ')
                        .Where(x => x.Length > 0)
                        .Select(x => UInt16.Parse(x));
                    
                    result.AddRange(numbers);
                }
            }

            return result;
        }
    }
}