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
            string token;

            manager.GetString(new Regex("^[ \t\n]+"));

            if((token = manager.GetString(new Regex("^[0-9]*[\\.][0-9]+[Ff]*"))) != null)
            {
                return new Token(TokenType.floating, token);
            } else if((token = manager.GetString(new Regex("^[0-9]+[fF]"))) != null)
            {
                return new Token(TokenType.floating, token);
            }
            else if ((token = manager.GetString(new Regex("^[0-9]+[uU]*"))) != null)
            {
                return new Token(TokenType.integer, token);
            }

            else if ((token = manager.GetString(new Regex("^[a-zA-Z_][a-zA-Z0-9_]+"))) != null)
            {
                return new Token(TokenType.identifier, token);
            }

            else if ((token = manager.GetString(new Regex("^[+*/()-]"))) != null){
                if(token == "+")
                {
                    return new Token(TokenType.add, "+");
                }

                else if(token == "-")
                {
                    return new Token(TokenType.sub, "-");
                }

                else if(token == "*")
                {
                    return new Token(TokenType.mul, "*");
                }

                else if(token == "/")
                {
                    return new Token(TokenType.div, "/");
                } else if(token == "(")
                {
                    return new Token(TokenType.lparen, "(");
                } else if(token == ")")
                {
                    return new Token(TokenType.rparen, ")");
                }
            }

            else if((token = manager.GetString(new Regex("^."))) != null)
            {
                return new Token(TokenType.invalid, "Invalid Token:" + token);
            }

            else
            {
                return new Token(TokenType.eof, "EOF");
            }

            return new Token(TokenType.invalid, "Invalid Token");


        }
    }
}
