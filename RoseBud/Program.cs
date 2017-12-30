using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer.Implementation;

namespace RoseBud
{
    static class Program
    {
        static Stack<Token> tokenStack;
        
        static void Main(string[] args)
        {
            List<Token> tokens = new RegexLexer(Console.ReadLine()).GetAllTokens();
            tokens.Reverse();

            tokenStack = new Stack<Token>(tokens);
            RosebudProgramAST program = RosebudProgram();
            Console.ReadKey();
        }

        private static RosebudProgramAST RosebudProgram()
        {
            RosebudProgramAST self = new RosebudProgramAST();
            self.entry = getEntry();
            RosebudImportAST import = null;
            while((import = getImport()) != null)
            {
                self.imports.Add(import);
            }
            RosebudFunctionAST function = null;
            while(tokenStack.Peek().GetTokenType() != "eof" && (function = getFunction()) != null)
            {
                self.functions.Add(function);
            }
            return self;

        }

        private static RosebudFunctionAST getFunction()
        {
            RosebudFunctionAST self = new RosebudFunctionAST();
            self.name = Eat("ident");
            Eat("divider", "(");
            Eat("divider", ")");
            Eat("divider", "{");
            Eat("divider", "}");
            return self;
        }

        private static RosebudImportAST getImport()
        {
            RosebudImportAST self = new RosebudImportAST();
            if(tokenStack.Peek().GetValue() != "import")
            {
                return null;
            }
            Eat("keyword", "import");
            self.module = getModule();
            Eat("eos");
            return self;
        }

        private static RosebudModuleAST getModule()
        {
            RosebudModuleAST self = new RosebudModuleAST();
            self.name = Eat("ident");
            if (tokenStack.Peek().GetTokenType() == "dot")
            {
                Eat("dot");
                self.submodule = getModule();
               
            } else
            {
                self.submodule = null;
            }
            return self;       
        }

        private static RosebudEntryAST getEntry()
        {
            if(tokenStack.Peek().GetValue() == "entry")
            {
                Eat("keyword", "entry");
                RosebudEntryAST self = new RosebudEntryAST();
                self.name = Eat("ident");
                Eat("eos");
                return self;
            } else
            {
                return null;
            }
        }

        static Token Eat(string type)
        {
            if(tokenStack.Peek().GetTokenType() == type)
            {
                return tokenStack.Pop();
            }
            throw new ArgumentException("Error, wrong token type, expected: " +
                type + " got: " + tokenStack.Peek().GetTokenType());

        }

        static Token Eat(string type, string value)
        {
            Token t = Eat(type);
            if(t.GetValue() == value)
            {
                return t;
            }
            throw new ArgumentException("Error, wrong token value, expected: " +
                value + "got: " + t.GetValue());
        }
        
    }
}
