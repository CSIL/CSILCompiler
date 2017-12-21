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

        public List<Token> getAllTokens()
        {
            Token t;
            List<Token> tokens = new List<Token>();
            while ((t = getNextToken()).GetTokenType() != TokenType.eof)
            {
                tokens.Add(t);
            }

            return tokens;
        }

    }
}
