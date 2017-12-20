using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    partial class Lexer
    {
        CodeManager manager;

        public Lexer(string code)
        {
            manager = new CodeManager(code: code);
        }

    }
}
