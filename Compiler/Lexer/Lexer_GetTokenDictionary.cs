using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compiler.Lexer
{
    partial class Lexer
    {
        public Token getNextTokenDictionary()
        {
            List<string> keywords = new List<string>()
            {
                "auto",     "double", "int",      "struct",
                "break",    "else",   "long",     "switch",
                "case",     "enum",   "register", "typedef",
                "char",     "extern", "return",   "union",
                "const",    "float",  "short",    "unsigned",
                "continue", "for",    "signed",   "void",
                "default",  "goto",   "sizeof",   "volatile",
                "do",       "if",     "static",   "while",
            };

            SortedDictionary<string, TokenType> tokenList = new SortedDictionary<string, TokenType>(new LengthComparer())
            {
                { "\"([^\"\\\\]|\\\\.)*\"", TokenType.string_constant },
                { "\'([^\'\\\\]|\\\\.)*\'", TokenType.character_constant },
                { "[0-9]*[\\.][0-9]+", TokenType.floating_constant },
                { "[0-9]+", TokenType.integer_constant },

                { "[a-zA-Z_][a-zA-Z0-9_]*", TokenType.identifier },

                { "\\+", TokenType.add },
                { "\\-", TokenType.sub },
                { "\\*", TokenType.mul },
                { "\\/", TokenType.div },
                { "\\|", TokenType.orop },
                { "&", TokenType.and },
                { "\\^", TokenType.exor },
                { ">>", TokenType.shr },
                { "<<", TokenType.shl },

                { "\\(", TokenType.lparen },
                { "\\)", TokenType.rparen },
                { "\\[", TokenType.lsqare },
                { "\\]", TokenType.rsqare },
                { "\\{", TokenType.rbracket },
                { "\\}", TokenType.lbracket },

                { "=", TokenType.assign },

                { ";", TokenType.eos },
                { "\\.", TokenType.dot },
            };

            // Ignore whitespace and comments
            manager.Get("[ \t\r\v\n]+");
            manager.Get("/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/");
            manager.Get("[ \t\r\n\n]+");

            string curtoken;
            foreach (KeyValuePair<string, TokenType> token in tokenList)
            {
                if((curtoken = manager.Get(token.Key)) != null)
                {
                    if(token.Value == TokenType.character_constant || token.Value == TokenType.string_constant)
                    {
                        return new Token(token.Value, curtoken.Remove(0,1).Remove(curtoken.Length-2, 1));
                    }
                    if (keywords.Contains(curtoken))
                    {
                        return new Token(TokenType.keyword, curtoken);
                    }
                    return new Token(token.Value, curtoken);
                }
                curtoken = null;
            }
            if ((curtoken = manager.Get(".")) != null)
            {
                return new Token(TokenType.invalid, curtoken);
            }
            return new Token(TokenType.eof, "EOF");
        }
    }
}
