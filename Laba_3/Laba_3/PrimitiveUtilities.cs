using System;
using System.Collections.Generic;

namespace Laba_3
{
    public static class PrimitiveUtilities
    {
        public static UInt16 GetPremetiveRoot(UInt16 p)
        {
            int phi = p - 1;

            var primeFactors = FindPrimeFactors(phi);

            for (var r = 2; r <= phi; r++)
            {
                bool flag = false;
                for (var i = 0; i < primeFactors.Count; i++)
                {
                    if (ModularPower(r, phi / primeFactors[i], p) == 1)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                    return (UInt16)r;
            }

            return 0;
        }

        public static List<int> FindPrimeFactors(int p)
        {
            var result = new List<int>();
            while (p % 2 == 0)
            {
               result.Add(2);
                p /= 2;
            }

            for (UInt16 i = 3; i <= Math.Sqrt(p); i += 2)
            {
                while (p % i == 0)
                {
                    result.Add(i);
                    p /= i;
                }
            }
            
            if (p > 2)
                result.Add(p);

            return result;
        }
        
        public static int ModularPower(int x, int y, int p) 
        { 
            int res = 1; 
  
            x = x % p;
  
            while (y > 0) 
            { 
                if (y % 2 == 1) 
                    res = (res*x) % p; 
  
                y = y >> 1; 
                x = (x * x) % p; 
            } 
            return res; 
        } 
    }
}