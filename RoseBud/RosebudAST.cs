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
    }

    public class RosebudFunctionAST
    {
        public Token name;
        public List<RosebudStatementAST> statements = new List<RosebudStatementAST>();
        public RosebudReturnAST rosebudReturn;
    }

    public class RosebudReturnAST
    {
        public RosebudExpressionAST expression;
    }

    public class RosebudExpressionAST
    {
        public RosebudTermAST left, right;
        public Token op;

    }

    public class RosebudTermAST
    {
        public RosebudFactorAST left, right;
        public Token op;
    }

    public class RosebudFactorAST
    {
        public RosebudFunctionCallAST toCall;
        public RosebudExpressionAST expression;
        public Token value;

    }

    public class RosebudFunctionCallAST
    {
    }

    public class RosebudStatementAST
    {
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
