using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Lexer;

namespace Compiler.Interpreter
{
    class MathCompiler
    {
        Lexer.Token[] tokens;
        int tokIndex;

        public MathCompiler(Lexer.Token[] tokens)
        {
            this.tokens = tokens;
        }

        public string MainMethod()
        {
            return this.Expression().ToString();
        }

        public void Eat(TokenType type)
        {
            if (tokIndex < tokens.Length)
            {
                if (tokens[tokIndex].GetTokenType() == type)
                {
                    tokIndex++;
                }
                else
                {
                    throw new Exception("Error, Invalid Token");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Error, tried to eat nonexistent token");
            }

        }

        public float Factor()
        {
            float result = 0;

            if (tokens[tokIndex].GetTokenType() == TokenType.integer || tokens[tokIndex].GetTokenType() == TokenType.floating)
            {
                if (tokens[tokIndex].GetTokenType() == TokenType.integer)
                {
                    Eat(TokenType.integer);
                    return float.Parse(tokens[tokIndex - 1].GetValue().TrimEnd("uU".ToArray()));
                }
                else
                {
                    Eat(TokenType.floating);
                    return float.Parse(tokens[tokIndex - 1].GetValue().TrimEnd("fF".ToArray()));
                }                
            }

            else if(tokens[tokIndex].GetTokenType() == TokenType.lparen)
            {
                Eat(TokenType.lparen);
                result = Expression();
                Eat(TokenType.rparen);
                return result;
            }

            throw new ArgumentException("Error, incorrect token encountered in parsing sequence");
        }

        public float Expression()
        {
            float result = Term();

            while(tokIndex < tokens.Length && (tokens[tokIndex].GetTokenType() == TokenType.add ||
                tokens[tokIndex].GetTokenType() == TokenType.sub))
            {
                if(tokens[tokIndex].GetTokenType() == TokenType.add)
                {
                    Eat(TokenType.add);
                    result += Term();
                } else if(tokens[tokIndex].GetTokenType() == TokenType.sub)
                {
                    Eat(TokenType.sub);
                    result -= Term();
                }
            }

            return result;
        }

        public float Term()
        {
            float result = Factor();
            while(tokIndex < tokens.Length && (tokens[tokIndex].GetTokenType() == TokenType.div ||
                tokens[tokIndex].GetTokenType() == TokenType.mul ||
                tokens[tokIndex].GetTokenType() == TokenType.mod))
            {
                if (tokens[tokIndex].GetTokenType() == TokenType.div)
                {
                    Eat(TokenType.div);
                    result /= Factor();
                }
                else if (tokens[tokIndex].GetTokenType() == TokenType.mul)
                {
                    Eat(TokenType.mul);
                    result *= Factor();
                } else if(tokens[tokIndex].GetTokenType() == TokenType.mod)
                {
                    Eat(TokenType.mod);
                    result = result % Factor();
                }
            }

            return result;
        }
    }
}
