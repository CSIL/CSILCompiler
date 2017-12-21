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
        public Token getNextToken()
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

            SortedDictionary<string, string> tokenList = new SortedDictionary<string, string>(new LengthComparer())
            {
                { "\"([^\"\\\\]|\\\\.)*\"", "string_constant" },
                { "\'([^\'\\\\]|\\\\.)*\'", "character_constant" },
                { "[0-9]*[\\.][0-9]+", "floating_constant" },
                { "[0-9]+", "integer_constant" },

                { "[a-zA-Z_][a-zA-Z0-9_]*", "identifier" },

                { "\\+", "add" },
                { "\\-", "sub" },
                { "\\*", "mul" },
                { "\\/", "div" },
                { "\\|", "orop" },
                { "&", "and" },
                { "\\^", "exor" },
                { ">>", "shr" },
                { "<<", "shl" },

                { "\\(", "lparen" },
                { "\\)", "rparen" },
                { "\\[", "lsqare" },
                { "\\]", "rsqare" },
                { "\\{", "rbracket" },
                { "\\}", "lbracket" },

                { "=", "assign" },

                { ";", "eos" },
                { "\\.", "dot" },
            };

            // Ignore whitespace and comments
            manager.Get("[ \t\r\v\n]+");
            manager.Get("/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/");
            manager.Get("[ \t\r\n\n]+");

            string curtoken;
            foreach (KeyValuePair<string, string> token in tokenList)
            {
                if((curtoken = manager.Get(token.Key)) != null)
                {
                    if(token.Value == "character_constant" || token.Value == "string_constant")
                    {
                        return new Token(token.Value, curtoken.Remove(0,1).Remove(curtoken.Length-2, 1));
                    }
                    if (keywords.Contains(curtoken))
                    {
                        return new Token("keyword", curtoken);
                    }
                    return new Token(token.Value, curtoken);
                }
                curtoken = null;
            }
            if ((curtoken = manager.Get(".")) != null)
            {
                return new Token("invalid", curtoken);
            }
            return new Token("eof", "EOF");
        }
    }
}
