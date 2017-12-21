using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Lexer;

namespace Compiler.Interpreter
{
    class CLangCompiler
    {
        Token[] tokens;
        public CLangCompiler(Token[] tokens)
        {
            this.tokens = tokens;
        }

        public void Parse()
        {

        }
    }
}
