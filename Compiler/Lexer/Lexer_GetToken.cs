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
            string current_token;
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

            // Ignore whitespace and comments
            manager.Get("[ \t\r\v\n]+");
            manager.Get("/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/");
            manager.Get("[ \t\r\n\n]+");

            // Get a string constant
            if ((current_token = manager.Get("\"([^\"\\\\]|\\\\.)*\"")) != null)
            {
                return new Token(TokenType.string_constant, current_token);
            }

            // get a character constant
            else if ((current_token = manager.Get("\'([^\'\\\\]|\\\\.)*\'")) != null)
            {
                return new Token(TokenType.character_constant, current_token);
            }

            // Get a floating point constant
            else if ((current_token = manager.Get("[0-9]*[\\.][0-9]+[Ff]?")) != null)
            {
                return new Token(TokenType.floating_constant, current_token);
            }
            // Also get a floating point constant expressed without a decimal
            else if ((current_token = manager.Get("[0-9]+[fF]")) != null)
            {
                return new Token(TokenType.floating_constant, current_token);
            }
            // Get an integer constant
            else if ((current_token = manager.Get("[0-9]+[uU]?")) != null)
            {
                return new Token(TokenType.integer_constant, current_token);
            }

            // Get an identifier
            else if ((current_token = manager.Get("[a-zA-Z_][a-zA-Z0-9_]*")) != null)
            {
                if (keywords.Contains(current_token))
                {
                    return new Token(TokenType.keyword, current_token);
                }
                return new Token(TokenType.identifier, current_token);
            }

            // get increment operator
            else if ((current_token = manager.Get("\\+\\+")) != null)
            {
                return new Token(TokenType.inc, "++");
            }

            else if ((current_token = manager.Get("\\-\\-")) != null)
            {
                return new Token(TokenType.dec, "--");
            }

            else if((current_token = manager.Get(">>")) != null)
            {
                return new Token(TokenType.shr, ">>");
            }

            else if((current_token = manager.Get("<<")) != null)
            {
                return new Token(TokenType.shl, "<<");
            }

            // Get an assignment operator
            else if ((current_token = manager.Get("[+*/&\\^;\\|-]=")) != null)
            {
                switch (current_token)
                {
                    case "+=":
                        return new Token(TokenType.assignplus, "+=");
                    case "-=":
                        return new Token(TokenType.assignminus, "-=");
                    case "*=":
                        return new Token(TokenType.assigntimes, "*=");
                    case "/=":
                        return new Token(TokenType.assigndiv, "/=");
                    case "&=":
                        return new Token(TokenType.assignand, "&=");
                    case "^=":
                        return new Token(TokenType.assignexor, "^=");
                    case "|=":
                        return new Token(TokenType.assignor, "|=");
                }
            }

            // Get one of the operators
            else if ((current_token = manager.Get("[+*/()&\\[\\]\\^;\\|=-]")) != null)
            {
                switch (current_token)
                {
                    case "+":
                        return new Token(TokenType.add, "+");
                    case "-":
                        return new Token(TokenType.sub, "-");
                    case "*":
                        return new Token(TokenType.mul, "*");
                    case "/":
                        return new Token(TokenType.div, "/");
                    case "(":
                        return new Token(TokenType.lparen, "(");
                    case ")":
                        return new Token(TokenType.rparen, ")");
                    case "&":
                        return new Token(TokenType.and, "&");
                    case "^":
                        return new Token(TokenType.exor, "^");
                    case "|":
                        return new Token(TokenType.orop, "|");
                    case ";":
                        return new Token(TokenType.eos, ";");
                    case "[":
                        return new Token(TokenType.lsqare, "[");
                    case "]":
                        return new Token(TokenType.rsqare, "]");
                    case "=":
                        return new Token(TokenType.assign, "=");
                }
            }


            else if ((current_token = manager.Get(".")) != null)
            {
                return new Token(TokenType.invalid, " token:" + current_token);
            }

            else
            {
                return new Token(TokenType.eof, "EOF");
            }

            return new Token(TokenType.invalid, "Invalid Token");


        }
    }
}
