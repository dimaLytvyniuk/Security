using System;

namespace Laba_2
{
    public class ArithmeticOperations
    {
        public static UInt32 AddByMod2In32(UInt32 a, UInt32 b)
        {
            return (UInt32) (checked (((UInt64)a + (UInt64)b)) % (UInt64)Math.Pow(2, 32));
        }

        public static UInt32 Circlular11LeftShift(UInt32 a)
        {
            UInt32 rightValue = (a & 0xFFE0_0000) >> 21;
            UInt32 leftValue = (UInt32) (checked ((a & 0x001F_FFFF) << 11));

            return leftValue | rightValue;
        }
    }
}