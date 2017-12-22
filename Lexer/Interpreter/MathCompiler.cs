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
        /// <summary>
        /// The List of tokens to be scanned
        /// </summary>
        Token[] tokens;

        /// <summary>
        /// The index of the current token
        /// </summary>
        int tokIndex;


        /// <summary>
        /// Create a calculator program to parse the 
        /// mathematical expression
        /// </summary>
        /// <param name="tokens">A sequence of tokens representing the expression</param>
        public MathCompiler(Token[] tokens)
        {
            this.tokens = tokens;
        }

        /// <summary>
        /// Scan the tokens to get the result
        /// </summary>
        /// <returns>The result of the program</returns>
        public string MainMethod()
        {
            try
            {
                return this.Expression().ToString();
            } catch
            {
                System.Windows.Forms.MessageBox.Show("Error in processing expression, please retry", "Error");
                return "";
            }
        }


        /// <summary>
        /// Get the correct type of token
        /// Throws an exception if the token is not the given type
        /// </summary>
        /// <param name="type">The type of the token to be matched</param>
        public void Eat(string type)
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
                throw new ArgumentOutOfRangeException("Error, tried to eat nonexistant token");
            }

        }


        /// <summary>
        /// A Mathematical factor
        /// Factor = Number | ( Expression )
        /// </summary>
        /// <returns>the number or result of evaluating the parenthesized expression</returns>
        public float Factor()
        {
            float result = 0;
            if(tokIndex >= tokens.Length)
            {
                throw new FormatException("Error, Not enough tokens to parse");
            }
            if (tokens[tokIndex].GetTokenType() == "integer_constant" || tokens[tokIndex].GetTokenType() == "floating_constant")
            {
                if (tokens[tokIndex].GetTokenType() =="integer_constant")
                {
                    Eat("integer_constant");
                    return float.Parse(tokens[tokIndex - 1].GetValue().TrimEnd("uU".ToArray()));
                }
                else
                {
                    Eat("floating_constant");
                    return float.Parse(tokens[tokIndex - 1].GetValue().TrimEnd("fF".ToArray()));
                }                
            }

            else if(tokens[tokIndex].GetTokenType() == "lparen")
            {
                Eat("lparen");
                result = Expression();
                Eat("rparen");
                return result;
            }

            throw new ArgumentException("Error, incorrect token encountered in parsing sequence");
        }

        /// <summary>
        /// A mathematical expression
        /// Expression = Term [(PLUS | MINUS) Term]*
        /// </summary>
        /// <returns>The result of evaluating the expression</returns>
        public float Expression()
        {
            float result = Term();

            while(tokIndex < tokens.Length && (tokens[tokIndex].GetTokenType() =="add" ||
                tokens[tokIndex].GetTokenType() =="sub"))
            {
                if(tokens[tokIndex].GetTokenType() == "add")
                {
                    Eat("add");
                    result += Term();
                } else if(tokens[tokIndex].GetTokenType() == "sub")
                {
                    Eat("sub");
                    result -= Term();
                }
            }

            return result;
        }

        /// <summary>
        /// A Mathematical term
        /// Term = Factor [(TIMES| DIVIDED) Term]*
        /// </summary>
        /// <returns>The result of evaluating the term</returns>
        public float Term()
        {
            float result = Factor();
            while(tokIndex < tokens.Length && (tokens[tokIndex].GetTokenType() == "div" ||
                tokens[tokIndex].GetTokenType() == "mul"))
            {
                if (tokens[tokIndex].GetTokenType() == "div")
                {
                    Eat("div");
                    result /= Factor();
                }
                else if (tokens[tokIndex].GetTokenType() == "mul")
                {
                    Eat("mul");
                    result *= Factor();
                }
            }

            return result;
        }
    }
}
