using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer;

namespace AST
{
    public class AST
    {
        protected Token self;

        public override string ToString()
        {
            return self.ToString();
        }
    }

    public class BinOpAST: AST
    {
        public AST left;
        public AST right;
        public Token op;

        public BinOpAST(AST left, AST right, Token op)
        {
            this.op = this.self = op;
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            return left.ToString() + " " + right.ToString() + " " + op.GetValue();
        }
    }

    public class NumAST: AST
    {
        float value;
        public NumAST(Token num)
        {
            this.self = num;
            this.value = float.Parse(this.self.GetValue());
        }

        public override string ToString()
        {
            return value.ToString();
        }

    }

        
}
