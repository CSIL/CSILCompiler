using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NoraSandlerC
{
    class Program
    {
        /* Grammar
         * <program> ::= <function>
         * <function> ::= "int" <id> "(" ")" "{" <statement> "}"
         * <statement> ::= "return" <exp> ";"
         * <exp> ::= <int>
         */

        static string assemblycode;
        static Lexer.Interfaces.IToken<string, string>[] tokens;
        static int index;

        static void Eat(string tokentype, string message = "Incorrect Token")
        {
            if(index < tokens.Length && tokens[index].GetTokenType() == tokentype)
            {
                index++;
            } else
            {
                throw new ArgumentException(message);
            }
        }
        
        static void Main(string[] args)
        {
            Console.Write("Input File: ");
            string fname = Console.ReadLine();
            Lexer.Interfaces.IStringLexer < Lexer.Interfaces.IToken<string, string>> tokenizer = new Lexer.Implementation.RegexLexer(System.IO.File.ReadAllText(fname));
            tokens = tokenizer.GetAllTokens().ToArray();

            SandlerProgramAST rootAST = CProgram();

            assemblycode = "";
            assemblycode += ".globl _" + rootAST.mainFunc.id.GetValue() +"\n";
            assemblycode += "_" + rootAST.mainFunc.id.GetValue() + ":\n";
            genOpCode(rootAST.mainFunc.statement.Expression);
            assemblycode += "\tret";
            Console.Write("Output File: ");
            string outfname = Console.ReadLine();
            System.IO.File.WriteAllText(outfname, assemblycode);

            Process.Start("gcc", "-m32 " + outfname + " -o out.exe");

        }

        private static void genOpCode(SandlerExpressionAST expression)
        {
            if(expression.op != null)
            {
                genOpCode(expression.exp);
                switch (expression.op.self.GetValue())
                {
                    case "-":
                        assemblycode += "\tneg %eax\n";
                        break;
                    case "~":
                        assemblycode += "\tnot %eax\n";
                        break;
                    case "!":
                        assemblycode += "\tcmpl $0, %eax\n";
                        assemblycode += "\tmovl $0, %eax\n";
                        assemblycode += "\tsete %al\n";
                        break;
                }
            } else
            {
                assemblycode += "\tmovl $" + expression.value.self.GetValue() + ", %eax\n";
            }
        }

        static SandlerProgramAST CProgram()
        {
            return new SandlerProgramAST(Function());
        }

        static SandlerFunctionAST Function()
        {
            if(tokens[index].GetTokenType() == "keyword" && tokens[index].GetValue() == "int")
            {
                Eat("keyword");
                Eat("identifier");
                Lexer.Interfaces.IToken<string, string> identifier = tokens[index - 1];
                Eat("lparen");
                Eat("rparen");
                Eat("lbracket");
                SandlerFunctionAST statement = new SandlerFunctionAST(identifier, Statement());
                Eat("rbracket");
                return statement;
            }
            throw new ArgumentException("Incorrect Token");
        }

        static SandlerStatementAST Statement()
        {
            if (tokens[index].GetTokenType() == "keyword" && tokens[index].GetValue() == "return")
            {
                Eat("keyword");
                SandlerStatementAST expression = new SandlerStatementAST(Expression());
                Eat("eos");
                return expression;
            }
            throw new ArgumentException("Incorrect Token");
        }

        static SandlerExpressionAST Expression()
        {
            if (new List<string>() { "negate", "not", "complement" }.Contains(tokens[index].GetTokenType()))
            {
                Eat(tokens[index].GetTokenType());
                int curindex = index-1;
                return new SandlerExpressionAST(new SandlerUnaryOpAST(tokens[curindex]), Expression(), null);
            }
            else
            {
                Eat("integer_constant");
                return new SandlerExpressionAST(null, null, new SandlerNumAST(tokens[index - 1]));
            }
            
        }
    }
}
