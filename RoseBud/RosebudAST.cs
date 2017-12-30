using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer.Implementation;

namespace RoseBud
{
    class RosebudProgramAST
    {
        public RosebudEntryAST entry;
        public List<RosebudImportAST> imports = new List<RosebudImportAST>();
        public List<RosebudFunctionAST> functions = new List<RosebudFunctionAST>();

        public override string ToString()
        {
            string value = entry.ToString();
            foreach(RosebudImportAST import in imports)
            {
                value += " " + import;
            }
            foreach(RosebudFunctionAST function in functions)
            {
                value += " " + function;
            }
            return value;
        }
    }

    public class RosebudFunctionAST
    {
        public Token name;
        public List<RosebudDeclarationAST> arguments;
        public List<RosebudStatementAST> statements = new List<RosebudStatementAST>();
        public RosebudReturnAST rosebudReturn;

        public override string ToString()
        {
            string value = name.ToString();
            foreach(RosebudDeclarationAST declaration in arguments)
            {
                value += " " + declaration;
            }
            value += "      ";
            foreach (RosebudStatementAST statement in statements)
            {
                value += " " + statement;
            }
            value += " " + rosebudReturn.ToString();
            return value;

        }
    }

    public class RosebudDeclarationAST
    {
        public Token name;
        public Token type;
        public RosebudMathValueAST value;

        public override string ToString()
        {
            return name + " " + type + " " + value;
        }
    }

    public class RosebudReturnAST
    {
        public RosebudMathValueAST expression;

        public override string ToString()
        {
            return expression.ToString();
        }
    }

    public class RosebudMathValueAST
    {
        public RosebudMathValueAST left, right; public Token op;
    }

    public class RosebudFactorAST: RosebudMathValueAST
    {
        public RosebudFunctionCallAST toCall;
        public RosebudMathValueAST expression;
        public Token value;

    }

    public class RosebudFunctionCallAST
    {
        public Token name;
        public List<RosebudVariableAST> arguments;
    }

    public class RosebudStatementAST
    {
        public RosebudVariableAST lvalue;
        public RosebudMathValueAST right;
        public RosebudVariableAST assign;
    }

    public class RosebudVariableAST
    {
        public Token name;
        public Token value;
    }

    public class RosebudImportAST
    {
        public RosebudModuleAST module;
    }

    public class RosebudModuleAST
    {
        public Token name;
        public RosebudModuleAST submodule;
    }

    public class RosebudEntryAST
    {
        public Token name;
    }
}
