using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClangCompiler
{
    class Program
    {
        /* Grammar
         * <program> ::= <function>
         * <function> ::= "int" <id> "(" ")" "{" <statement> "}"
         * <statement> ::= "return" <exp> ";"
         * <exp> ::= <int>
         */

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

        static string assemblyCode;

        static void Main(string[] args)
        {
            Console.Write("Input File: ");
            string fname = Console.ReadLine();
            Lexer.Interfaces.IStringLexer<Lexer.Interfaces.IToken<string, string>> tokenizer = new Lexer.Implementation.RegexLexer(System.IO.File.ReadAllText(fname));
            tokens = tokenizer.GetAllTokens().ToArray();

            CProgram();

            
            string outfname = fname.Remove(fname.Length - 1, 1) + "s";
            System.IO.File.WriteAllText(outfname, assemblyCode);
            string exefname = outfname.Remove(outfname.Length - 1, 1) + "exe";
            Process p = Process.Start("gcc", "-m32 " + outfname + " -o " + exefname);
            //System.IO.File.Delete(outfname);
        }

        
        

        static void CProgram()
        {
            while (index < tokens.Length && tokens[index].GetTokenType() == "keyword")
            {
                Function();
            }
        }

        static void Function()
        {
            if(tokens[index].GetTokenType() == "keyword" && tokens[index].GetValue() == "int")
            {
                Eat("keyword");
                assemblyCode += ".globl _" + tokens[index].GetValue() + "\n";
                Eat("identifier");
                Lexer.Interfaces.IToken<string, string> identifier = tokens[index - 1];
                assemblyCode += "_" + identifier.GetValue() + ":" + "\n";
                Eat("lparen");
                Eat("rparen");
                Eat("lbracket");
                Statement();
                Eat("rbracket");
            }
        }

        static void Statement()
        {
            Eat("keyword");
            Expression();
            assemblyCode += "\tret\n";
            Eat("eos");
        }

        static void Expression()
        {
            Term();
            while (new List<string>() { "plus", "minus" }.Contains(tokens[index].GetTokenType()))
            {
                assemblyCode += "\tpush %eax\n";
                Lexer.Interfaces.IToken<string, string> token = tokens[index];
                Eat(tokens[index].GetTokenType());
                Term();
                assemblyCode += "\tpop %ecx\n";
                if (token.GetTokenType() == "plus")
                {
                    assemblyCode += "\taddl %ecx, %eax\n";
                }
                else
                {
                    assemblyCode += "\tsubl %ecx, %eax\n";
                    assemblyCode += "\tneg %eax\n";
                }
            }
        }

        private static void Term()
        {
            Factor();
            while (new List<string>() { "mul", "div" }.Contains(tokens[index].GetTokenType()))
            {
                assemblyCode += "\tpush %eax\n";
                Lexer.Interfaces.IToken<string, string> token = tokens[index];
                Eat(tokens[index].GetTokenType());
                Factor();

                if (token.GetTokenType() == "mul")
                {
                    assemblyCode += "\tpop %ecx\n";
                    assemblyCode += "\timul %ecx, %eax\n";
                }
                else
                {
                    assemblyCode += "\txorl %edx, %edx\n";
                    assemblyCode += "\tpop %ecx\n";
                    assemblyCode += "\tidivl %ecx\n";
                }
                
            }
        }

        private static void Factor()
        {
            if(tokens[index].GetTokenType() == "lparen")
            {
                Eat("lparen");
                Expression();
                Eat("rparen");
            }
            else if(new List<string>() { "minus", "complement", "not", "plus", "inc", "dec" }.Contains(tokens[index].GetTokenType()))
            {
                int ind = index;
                Eat(tokens[index].GetTokenType());
                Factor();
                UnaryOp(tokens[ind]);
            }
            else if(tokens[index].GetTokenType() == "identifier")
            {
                assemblyCode += "\tcall _" + tokens[index].GetValue() + "\n";
                Eat("identifier");
                Eat("lparen");
                Eat("rparen");
            }
            else
            {
                Eat("integer_constant");
                assemblyCode += "\tmovl $" + tokens[index-1].GetValue() + ", %eax\n";
            }
        }

        private static void UnaryOp(Lexer.Interfaces.IToken<string, string> token)
        {
            switch (token.GetValue())
            {
                case "-":
                    assemblyCode += "\tneg %eax\n";
                    break;
                case "+":
                    break;
                case "~":
                    assemblyCode += "\tnot %eax\n";
                    break;
                case "!":
                    assemblyCode += "\tcmpl $0, %eax\n";
                    assemblyCode += "\tmovl $0, %eax\n";
                    assemblyCode += "\tsete %al\n";
                    break;
                case "++":
                    assemblyCode += "\tinc %eax\n";
                    break;
                case "--":
                    assemblyCode += "\tdec %eax\n";
                    break;

            }
        }
    }
}
