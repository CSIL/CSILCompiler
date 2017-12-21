using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    class LengthComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int lengthComparison = y.Length.CompareTo(x.Length);
            if(lengthComparison == 0)
            {
                return string.Compare(y, x, StringComparison.Ordinal);
            } else
            {
                return lengthComparison;
            }
        }
    }
}
