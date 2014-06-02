//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace Scott.Utilities
{
    class SCUtil
    {
        //Note to self: organize functions alphabetically
        static public int Max(int A, int B)
        {
            if (B > A)
                return B;
            else
                return A;
        }

        static public int Min(int A, int B)
        {
            if (B < A)
                return B;
            else
                return A;
        }
    }
}
