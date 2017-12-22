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
        protected Lexer.Interfaces.IToken<string, string> self;

        public override string ToString()
        {
            return self.ToString();
        }
    }

    public class BinOpAST: AST
    {
        protected AST left;
        protected AST right;

        public BinOpAST(AST left, AST right, Lexer.Interfaces.IToken<string, string> op)
        {
            this.self = op;
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            return left.ToString() + " " + right.ToString() + " " + self.GetValue();
        }
    }

    public class NumAST: AST
    {
        float value;
        public NumAST(Lexer.Interfaces.IToken<string, string> num)
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
