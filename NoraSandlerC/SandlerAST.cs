using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoraSandlerC
{
    class SandlerAST
    {
    }
    
    class SandlerExpressionAST : SandlerAST
    {
        public SandlerNumAST value;
        public SandlerExpressionAST exp;
        public SandlerUnaryOpAST op;

        public SandlerExpressionAST(SandlerUnaryOpAST op = null, SandlerExpressionAST exp = null, SandlerNumAST value = null)
        {
            this.exp = exp;
            this.value = value;
            this.op = op;
        }

    }

    public class SandlerUnaryOpAST
    {
        public Lexer.Interfaces.IToken<string, string> self;
        public SandlerUnaryOpAST(Lexer.Interfaces.IToken<string, string> op)
        {
            this.self = op;
        }
    }

    class SandlerNumAST:SandlerAST
    {
        public Lexer.Interfaces.IToken<string, string> self;
        public SandlerNumAST(Lexer.Interfaces.IToken<string, string> token)
        {
            this.self = token;
        }
    }

    class SandlerStatementAST: SandlerAST
    {
        public SandlerExpressionAST Expression;

        public SandlerStatementAST(SandlerExpressionAST Expression)
        {
            this.Expression = Expression;
        }
    }

    class SandlerFunctionAST: SandlerAST
    {
        public Lexer.Interfaces.IToken<string, string> id;
        public SandlerStatementAST statement;

        public SandlerFunctionAST(Lexer.Interfaces.IToken<string, string> id, SandlerStatementAST statement)
        {
            this.statement = statement;
            this.id = id;
        }
    }

    class SandlerProgramAST: SandlerAST
    {
        public SandlerFunctionAST mainFunc;

        public SandlerProgramAST(SandlerFunctionAST mainFunc)
        {
            this.mainFunc = mainFunc;
        }
    }
}

