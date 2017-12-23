using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer.Interfaces;

namespace ClangCompiler
{
    class Program
    {
        static int tokIndex = 0;
        static Dictionary<string, int> intvars = new Dictionary<string, int>();
        static Dictionary<string, float> floatvars = new Dictionary<string, float>();
        static private IToken<string, string>[] tokens;
        static string code = "";
        static string curcode = "";

        static void Main(string[] args)
        {
            while (true)
            {
                code = "";
                while ((curcode = Console.ReadLine()) != "")
                {
                    code += curcode;
                }
                Lexer.Interfaces.IStringLexer<Lexer.Interfaces.IToken<string, string>> tokenizer = new Lexer.Implementation.RegexLexer(code);
                List<Lexer.Interfaces.IToken<string, string>> tokens = tokenizer.GetAllTokens();
                try
                {
                    Syntax(tokens.ToArray());
                    Console.WriteLine("Integer Variables");
                    foreach (KeyValuePair<string, int> variable in intvars)
                    {
                        Console.WriteLine(variable.Key + " " + variable.Value);
                    }
                    Console.WriteLine("\nFloating Variables");
                    foreach (KeyValuePair<string, float> variable in floatvars)
                    {
                        Console.WriteLine(variable.Key + " " + variable.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void Eat(string type, string errorMessage = "Wrong Token Type")
        {
            if (tokIndex < tokens.Length && tokens[tokIndex].GetTokenType() == type)
            {
                tokIndex++;
            }
            else
            {
                throw new ArgumentException(errorMessage);
            }
        }

        static void Syntax(Lexer.Interfaces.IToken<string, string>[] Tokens)
        {
            tokens = Tokens;
            while (tokIndex < tokens.Length && new List<string>() { "keyword", "identifier" }.Contains(tokens[tokIndex].GetTokenType()))
            {
                if (tokens[tokIndex].GetTokenType() == "keyword")
                {
                    Eat("keyword");
                    if (tokens[tokIndex - 1].GetValue() == "int")
                    {
                        if (tokens[tokIndex].GetTokenType() == "identifier")
                        {
                            Eat("identifier", "Error, you cannot create an unnamed variable");
                            Eat("assign", "Error you currently cannot create a variable without a value");
                            if (!intvars.ContainsKey(tokens[tokIndex - 2].GetValue()) && !floatvars.ContainsKey(tokens[tokIndex - 2].GetValue()))
                            {
                                intvars.Add(tokens[tokIndex - 2].GetValue(), 0);
                            }
                            if (!floatvars.ContainsKey(tokens[tokIndex - 2].GetValue()))
                            {
                                intvars[tokens[tokIndex - 2].GetValue()] = (int)Expression();
                            }
                            else
                            {
                                throw new Exception("Cannot create new variable with the same name");
                            }
                            Eat("eos");
                        }
                    }
                    else if (tokens[tokIndex - 1].GetValue() == "float")
                    {
                        if (tokens[tokIndex].GetTokenType() == "identifier")
                        {
                            Eat("identifier", "Error You cannot create an unnamed variable");
                            Eat("assign", "Error, you cannot create an unnamed variable without a value");
                            if (!floatvars.ContainsKey(tokens[tokIndex - 2].GetValue()) && !intvars.ContainsKey(tokens[tokIndex - 2].GetValue()))
                            {
                                floatvars.Add(tokens[tokIndex - 2].GetValue(), 0);
                            }
                            if (!intvars.ContainsKey(tokens[tokIndex - 2].GetValue()))
                            {
                                floatvars[tokens[tokIndex - 2].GetValue()] = Expression();
                            }
                            else
                            {
                                throw new Exception("Cannot create new variable with the same name");
                            }
                            Eat("eos", "Error, semicolon expected");
                        }
                    }
                } else
                {
                    if (tokens[tokIndex].GetTokenType() == "identifier")
                    {
                        Eat("identifier", "Error, lvalue expected");
                        Eat("assign", "you cannot just state a variable name");
                        if (intvars.ContainsKey(tokens[tokIndex - 2].GetValue()))
                        {
                            intvars[tokens[tokIndex - 2].GetValue()] = (int)Expression();
                        }
                        else if(floatvars.ContainsKey(tokens[tokIndex-2].GetValue()))
                        {
                            floatvars[tokens[tokIndex - 2].GetValue()] = Expression();
                        } else
                        {
                            throw new Exception("Error: Attempted to assign unidentified variable");
                        }
                        Eat("eos", "error, semicolon expected");
                    }
                }
            }
        }

        private static float Expression()
        {
            float value = Term();
            while (tokIndex< tokens.Length && new List<string>() {"add", "sub" }.Contains(tokens[tokIndex].GetTokenType()))
            {
                Eat(tokens[tokIndex].GetTokenType(), "Error, expected ADDop");
                if(tokens[tokIndex-1].GetValue() == "+")
                {
                    value += Term();
                } else
                {
                    value -= Term();
                }
            }
            return value;
        }

        private static float Term()
        {
            float value = Factor();
            while (tokIndex< tokens.Length && new List<string>() { "mul", "div" }.Contains(tokens[tokIndex].GetTokenType()))
            {
                Eat(tokens[tokIndex].GetTokenType(), "Error, expected MULop");
                if (tokens[tokIndex - 1].GetValue() == "*")
                {
                    value *= Factor();
                }
                else
                {
                    value /= Factor();
                }
            }
            return value;
        }

        private static float Factor()
        {
            if (tokens[tokIndex].GetTokenType() == "integer_constant")
            {
                Eat("integer_constant");
                return float.Parse(tokens[tokIndex - 1].GetValue());
            } else if (tokens[tokIndex].GetTokenType() == "lparen")
            {
                Eat("lparen");
                float value = Expression();
                Eat("rparen");
                return value;
            } else if (tokens[tokIndex].GetTokenType() == "identifier")
            {
                if (intvars.ContainsKey(tokens[tokIndex].GetValue()))
                {
                    Eat("identifier");
                    return intvars[tokens[tokIndex - 1].GetValue()];
                } else if (floatvars.ContainsKey(tokens[tokIndex - 1].GetValue()))
                {
                    Eat("identifier");
                    return floatvars[tokens[tokIndex - 1].GetValue()];
                } else             
                {
                    throw new Exception("Undefined value encountered while parsing");
                }
            }
            throw new Exception("Error parsing token");
        }
    }
}
