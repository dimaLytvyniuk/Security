using System;
using System.Collections.Generic;

namespace Laba_2
{
    public class GhostAlgorithm
    {
        public static UInt64[] EncodeMessage(UInt64[] message, UInt32[] key, UInt64[] substitionTable)
        {
            var encodedMessage = new UInt64[message.Length];
            
            var keyDictionary = BuildKeyEncodeDictionary(key);
            IReadOnlyDictionary<int, IReadOnlyList<byte>> substitionTableDictionary = BuildSubstitionTableDictionary(substitionTable);
            
            for (var i = 0; i < message.Length; i++)
            {
                var encodedValue = Encode64Bits(message[i], keyDictionary, substitionTableDictionary);
                encodedMessage[i] = encodedValue;
            }
            
            return encodedMessage;
        }

        public static UInt64[] DecodeMessage(UInt64[] message, UInt32[] key, UInt64[] substitionTable)
        {
            var decodedMessage = new UInt64[message.Length];

            var keyDictionary = BuildKeyDecodeDictionary(key);
            IReadOnlyDictionary<int, IReadOnlyList<byte>> substitionTableDictionary = BuildSubstitionTableDictionary(substitionTable);
            
            for (var i = 0; i < message.Length; i++)
            {
                var decodedValue = Decode64Bits(message[i], keyDictionary, substitionTableDictionary);
                decodedMessage[i] = decodedValue;
            }
            
            return decodedMessage;
        }

        public static UInt64 Encode64Bits(UInt64 message, Dictionary<int, UInt32> keyDictionary, IReadOnlyDictionary<int, IReadOnlyList<byte>> substiotionDictionary)
        {
            var N1 = (UInt32) ((message & 0xFFFF_FFFF_0000_0000) >> 32);
            var N2 = (UInt32) (message & 0x0000_0000_FFFF_FFFF);

            var first = N1;
            var second = N2;

            UInt32 CM1, K, R;
            for (var i = 0; i < 31; i++)
            {
                CM1 = ArithmeticOperations.AddByMod2In32(N1, keyDictionary[i]);
                K = MakeSubstition(CM1, substiotionDictionary);
                R = ArithmeticOperations.Circlular11LeftShift(K);
                UInt32 CM2 = N2 ^ R;
                N2 = N1;
                N1 = CM2;
            }
            CM1 = ArithmeticOperations.AddByMod2In32(N1, keyDictionary[31]);
            K = MakeSubstition(CM1, substiotionDictionary);
            R = ArithmeticOperations.Circlular11LeftShift(K);
            N2 = N2 ^ R;

            UInt64 result = N1;
            result <<= 32;
            result |= N2;
            
            return result;
        }

        public static UInt64 Decode64Bits(UInt64 message, Dictionary<int, UInt32> keyDictionary, IReadOnlyDictionary<int, IReadOnlyList<byte>> substiotionDictionary)
        {
            var N1 = (UInt32) ((message & 0xFFFF_FFFF_0000_0000) >> 32);
            var N2 = (UInt32) (message & 0x0000_0000_FFFF_FFFF);
            
            UInt32 K, CM1, R;
            for (var i = 0; i < 31; i++)
            {
                CM1 = ArithmeticOperations.AddByMod2In32(N1, keyDictionary[i]);
                K = MakeSubstition(CM1, substiotionDictionary);
                R = ArithmeticOperations.Circlular11LeftShift(K);
                UInt32 CM2 = N2 ^ R;
                N2 = N1;
                N1 = CM2;
            }
            CM1 = ArithmeticOperations.AddByMod2In32(N1, keyDictionary[31]);
            K = MakeSubstition(CM1, substiotionDictionary);
            R = ArithmeticOperations.Circlular11LeftShift(K);
            N2 = N2 ^ R;
            
            UInt64 result = N1;
            result <<= 32;
            result |= N2;
            
            return result;
        }

        public static UInt32 MakeSubstition(UInt32 number,
            IReadOnlyDictionary<int, IReadOnlyList<byte>> substiotionDictionary)
        {
            IReadOnlyList<byte> vector = DropOn4BytesVector(number);
            UInt32 outputNumber = 0; 
            
            for (var i = 0; i < 7; i++)
            {
                outputNumber += substiotionDictionary[i][vector[i]];
                outputNumber <<= 4;
            }
            outputNumber += substiotionDictionary[7][vector[7]];
            
            return outputNumber;
        }
        
        public static Dictionary<int, UInt32> BuildKeyEncodeDictionary(UInt32[] key)
        {
            var result = new Dictionary<int, UInt32>(32);

            for (var i = 0; i < 24; i++)
            {
                result[i] = key[i % 8];
            }

            for (var i = 24; i < 32; i++)
            {
                result[i] = key[31 - i];
            }

            return result;
        }
        
        public static Dictionary<int, UInt32> BuildKeyDecodeDictionary(UInt32[] key)
        {
            var result = new Dictionary<int, UInt32>(32);

            for (var i = 0; i < 8; i++)
            {
                result[i] = key[i];
            }
            
            for (var i = 8; i < 32; i++)
            {
                result[i] = key[7 - (i % 8)];
            }

            return result;
        }

        public static IReadOnlyDictionary<int, IReadOnlyList<byte>> BuildSubstitionTableDictionary(
            UInt64[] substitionTable)
        {
            var result = new Dictionary<int, IReadOnlyList<byte>>();

            for (var i = 0; i < substitionTable.Length; i++)
            {
                var list = new List<byte>(16);
                var shiftValue = 0xF000_0000_0000_0000;
                var shiftSubstitionValue = 60;                
                for (var j = 0; j < 15; j++)
                {
                    list.Add((byte)((substitionTable[i] & shiftValue) >> shiftSubstitionValue));
                    shiftValue >>= 4;
                    shiftSubstitionValue -= 4;
                }
                list.Add((byte)(substitionTable[i] & 0x0000_0000_0000_000F));
                result[i] = list;
            }

            return result;
        }

        public static IReadOnlyList<byte> DropOn4BytesVector(UInt32 vector)
        {
            var result = new byte[8];
            
            var shiftValue = 0xF000_0000;
            var shiftSubstitionValue = 28;                
            for (var i = 0; i < 7; i++)
            {
                result[i] = ((byte)((vector & shiftValue) >> shiftSubstitionValue));
                shiftValue >>= 4;
                shiftSubstitionValue -= 4;
            }
            result[7] = ((byte)(vector & 0x0000_000F));
            
            return result;
        }
    }
}
