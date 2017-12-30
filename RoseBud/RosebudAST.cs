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
        private RosebudEntryAST entry;
        private List<RosebudImportAST> imports = new List<RosebudImportAST>();
        private List<RosebudFunctionAST> functions = new List<RosebudFunctionAST>();

        public RosebudEntryAST Entry { get => entry; set => entry = value; }
        public List<RosebudImportAST> Imports { get => imports; set => imports = value; }
        public List<RosebudFunctionAST> Functions { get => functions; set => functions = value; }

        public override string ToString()
        {
            string value = Entry.ToString();
            foreach(RosebudImportAST import in Imports)
            {
                value += " " + import;
            }
            foreach(RosebudFunctionAST function in Functions)
            {
                value += " " + function;
            }
            return value;
        }
    }

    public class RosebudFunctionAST
    {
        private Token name;
        private List<RosebudDeclarationAST> arguments;
        private List<RosebudStatementAST> statements = new List<RosebudStatementAST>();
        private RosebudReturnAST rosebudReturn;

        public Token Name { get => name; set => name = value; }
        public List<RosebudDeclarationAST> Arguments { get => arguments; set => arguments = value; }
        public List<RosebudStatementAST> Statements { get => statements; set => statements = value; }
        public RosebudReturnAST RosebudReturn { get => rosebudReturn; set => rosebudReturn = value; }

        public override string ToString()
        {
            string value = Name.ToString();
            foreach(RosebudDeclarationAST declaration in Arguments)
            {
                value += " " + declaration;
            }
            value += "      ";
            foreach (RosebudStatementAST statement in Statements)
            {
                value += " " + statement;
            }
            value += " " + RosebudReturn.ToString();
            return value;

        }
    }

    public class RosebudDeclarationAST
    {
        private Token name;
        private Token type;
        private RosebudMathValueAST value;

        public Token Name { get => name; set => name = value; }
        public Token Type { get => type; set => type = value; }
        public RosebudMathValueAST Value { get => value; set => this.value = value; }

        public override string ToString()
        {
            return Name + " " + Type + " " + Value;
        }
    }

    public class RosebudReturnAST
    {
        private RosebudMathValueAST expression;

        public RosebudMathValueAST Expression { get => expression; set => expression = value; }

        public override string ToString()
        {
            return Expression.ToString();
        }
    }

    public class RosebudMathValueAST
    {
        private RosebudMathValueAST right;
        private Token op;
        private RosebudMathValueAST left;

        public RosebudMathValueAST Left { get => left; set => left = value; }
        public RosebudMathValueAST Right { get => right; set => right = value; }
        public Token Op { get => op; set => op = value; }
    }

    public class RosebudFactorAST: RosebudMathValueAST
    {
        private RosebudFunctionCallAST toCall;
        private RosebudMathValueAST expression;
        private Token value;

        public RosebudFunctionCallAST ToCall { get => toCall; set => toCall = value; }
        public RosebudMathValueAST Expression { get => expression; set => expression = value; }
        public Token Value { get => value; set => this.value = value; }
    }

    public class RosebudFunctionCallAST
    {
        private Token name;
        private List<RosebudVariableAST> arguments;

        public List<RosebudVariableAST> Arguments { get => arguments; set => arguments = value; }
        public Token Name { get => name; set => name = value; }
    }

    public class RosebudStatementAST
    {
        private RosebudVariableAST lvalue;
        private RosebudMathValueAST right;
        private RosebudVariableAST assign;

        public RosebudMathValueAST Right { get => right; set => right = value; }
        public RosebudVariableAST Assign { get => assign; set => assign = value; }
        public RosebudVariableAST Lvalue { get => lvalue; set => lvalue = value; }
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
