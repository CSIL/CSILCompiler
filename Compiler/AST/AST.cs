using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compiler.Lexer;

namespace Compiler.AST
{
    class AST
    {
        protected Token token;
    }

    class BinOP : AST
    {
        Token left, right, op;

        public BinOP(Token left, Token op , Token right)
        {
            this.left = left;
            this.right = right;
            this.op = this.token = op;
        }
        
    }

    class Num : AST
    {
        Token value;
        public Num(Token value)
        {
            this.token = this.value = value;
        }
    }
}
