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

        }

        /// <summary>
        /// Build a rosebud program from functions import statements and entry statements
        /// </summary>
        /// <returns>a complete ast for the program</returns>
        private static RosebudProgramAST RosebudProgram()
        {
            RosebudProgramAST self = new RosebudProgramAST
            {
                // Get either the entry point of the program or null
                entry = GetEntry()
            };
            if (self.entry == null)
            {
                // if null, create the default entry of main
                self.entry = new RosebudEntryAST { name = new Token("ident", "main") };
            }


            // create a blank import node
            RosebudImportAST import = null;
            // keep adding imports to the list if there are any
            while((import = GetImport()) != null)
            {
                self.imports.Add(import);
            }
            // create a blank function node
            RosebudFunctionAST function = null;
            // keep adding functions to the list if there are any
            while(tokenStack.Peek().GetTokenType() != "eof" && (function = GetFunction()) != null)
            {
                self.functions.Add(function);
            }
            // return the program
            return self;

        }

        /// <summary>
        /// Create an function AST node
        /// </summary>
        /// <returns>the composited function</returns>
        private static RosebudFunctionAST GetFunction()
        {
            // create a blank function
            RosebudFunctionAST self = new RosebudFunctionAST
            {
                // get a name for the function
                name = Eat("ident")
            };
            // get rid of the parenthesis
            Eat("divider", "(");
            // get a list of arguments for the function
            self.arguments = GetFunctionArgs();
            // get rid of the next dividers
            Eat("divider", ")");
            Eat("divider", "{");
            // get the return statement
            // TODO: Allow doing normal statements inside the function
            self.rosebudReturn = GetReturn();
            // get rid of the closing bracket
            Eat("divider", "}");
            // return the function
            return self;
        }

        /// <summary>
        /// Gets a return statement
        /// </summary>
        /// <returns>an AST node representing the return value</returns>
        private static RosebudReturnAST GetReturn()
        {
            // create a blank return value
            RosebudReturnAST self = new RosebudReturnAST();
            // get rid of the return statement
            Eat("keyword", "return");
            // get the expression that is the return statement
            self.expression = GetExpression();
            // get rid of the semicolon ending the statement
            Eat("eos");
            // return the return
            return self;
        }

        /// <summary>
        /// Gets a list of arguments for the function with possible default values
        /// </summary>
        /// <returns>a list of arugment AST node</returns>
        private static List<RosebudDeclarationAST> GetFunctionArgs()
        {
            // create a blank list for the arguments
            List<RosebudDeclarationAST> self = new List<RosebudDeclarationAST>();
            // check if there is an argument
            if (tokenStack.Peek().GetTokenType() == "keyword")
            {
                // get a declaration and add it to the list
                RosebudDeclarationAST first = GetDeclaration();
                self.Add(first);
            }

            // scan through the rest and get the other arguments
            while(tokenStack.Peek().GetTokenType() == "comma")
            {
                // get rid of the comma
                Eat("comma");
                // get the next declaration and add it
                RosebudDeclarationAST next = GetDeclaration();
                self.Add(next);
            }
            // return the arguments
            return self;
        }

        /// <summary>
        /// Creates a declaration node
        /// </summary>
        /// <returns>an AST node representing the declaration</returns>
        private static RosebudDeclarationAST GetDeclaration()
        {
            // create a blank declaration and initialize it
            RosebudDeclarationAST self = new RosebudDeclarationAST()
            {
                type = Eat("keyword", "int"),
                name = Eat("ident"),
            };
            // check for an assignment statement
            if (tokenStack.Peek().GetTokenType() == "assign")
            {
                Eat("assign");
                // get an expression to assign
                self.value = GetExpression();
            }
            else
            {
                // set the default value to nothing
                self.value = null;
            }
            // return the declaration
            return self;
        }

        /// <summary>
        /// Get an expression off of the stack
        /// </summary>
        /// <returns>an AST node representing the expression</returns>
        // TODO Fix up the way this works
        private static RosebudMathValueAST GetExpression()
        {
            // create a blank expression
            RosebudMathValueAST self = new RosebudMathValueAST
            {
                // get a multiplicative expression to evaluate
                left = GetTerm()
            };
            // recursively scan to 
            /*
            while(new List<string> { "plus", "minus" }.Contains(tokenStack.Peek().GetTokenType())){
                self.op = tokenStack.Pop();
                self.right = new RosebudMathValueAST()
                {
                    left = self.right,
                    op = self.op,
                    right = getExpression(),
                };
            }
            */
            // recursively subdivide into terms
            if (new List<string> {"plus", "minus" }.Contains(tokenStack.Peek().GetTokenType()))
            {
                self.op = Eat(tokenStack.Peek().GetTokenType());
                self.right = GetExpression();
            }
            return self;
        }

        private static RosebudMathValueAST GetTerm()
        {
            RosebudMathValueAST self = new RosebudMathValueAST
            {
                left = GetFactor()
            };
            /*
            while (new List<string> { "mul", "div" }.Contains(tokenStack.Peek().GetTokenType()))
            {
                self.op = tokenStack.Pop();
                self.right = new RosebudMathValueAST()
                {
                    left = self.right,
                    op = self.op,
                    right = getExpression(),
                };
            }
            */
            // recursively get factors
            if (new List<string> { "mul", "div" }.Contains(tokenStack.Peek().GetTokenType())){
                self.op = Eat(tokenStack.Peek().GetTokenType());
                self.right = GetFactor();
            }
            
            return self;
        }

        private static RosebudFactorAST GetFactor()
        {
            RosebudFactorAST self = new RosebudFactorAST();
            if (tokenStack.Peek().GetValue() == "(")
            {
                Eat("divider", "(");
                // evalueate inside parentheses first
                self.expression = GetExpression();
                Eat("divider", ")");
            }
            else if(tokenStack.Peek().GetTokenType() == "int")
            {
                self.value = Eat("int");
            }
            else
            {
                self.toCall = GetCall();
            }

            return self;
        }

        private static RosebudFunctionCallAST GetCall()
        {
            RosebudFunctionCallAST self = new RosebudFunctionCallAST
            {
                name = Eat("ident")
            };
            Eat("divider", "(");
            Eat("divider", ")");
            return self;

        }

        private static RosebudImportAST GetImport()
        {
            RosebudImportAST self = new RosebudImportAST();
            if(tokenStack.Peek().GetValue() != "import")
            {
                return null;
            }
            Eat("keyword", "import");
            self.module = GetModule();
            Eat("eos");
            return self;
        }

        private static RosebudModuleAST GetModule()
        {
            RosebudModuleAST self = new RosebudModuleAST
            {
                name = Eat("ident")
            };
            if (tokenStack.Peek().GetTokenType() == "dot")
            {
                Eat("dot");
                self.submodule = GetModule();
               
            } else
            {
                self.submodule = null;
            }
            return self;       
        }

        private static RosebudEntryAST GetEntry()
        {
            if(tokenStack.Peek().GetValue() == "entry")
            {
                Eat("keyword", "entry");
                RosebudEntryAST self = new RosebudEntryAST
                {
                    name = Eat("ident")
                };
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

        static void Eat(string type, string value)
        {
            Token t = Eat(type);
            if(t.GetValue() == value)
            {
                return;
            }
            throw new ArgumentException("Error, wrong token value, expected: " +
                value + "got: " + t.GetValue());
        }
        
    }
}
