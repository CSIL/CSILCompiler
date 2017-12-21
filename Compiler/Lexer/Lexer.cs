using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    partial class Lexer
    {
        CodeManager manager;

        string comment_sequence = "/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/";

        /// <summary>
        /// Default language keywords
        /// </summary>
        List<string> keywords = new List<string>()
        {
                "auto",     "double", "int",      "struct",
                "break",    "else",   "long",     "switch",
                "case",     "enum",   "register", "typedef",
                "char",     "extern", "return",   "union",
                "const",    "float",  "short",    "unsigned",
                "continue", "for",    "signed",   "void",
                "default",  "goto",   "sizeof",   "volatile",
                "do",       "if",     "static",   "while",
        };

        /// <summary>
        /// Default allowed tokens
        /// </summary>
        SortedDictionary<string, string> allowed_tokens = new SortedDictionary<string, string>(new LengthComparer())
            {
                { "\"([^\"\\\\]|\\\\.)*\"", "string_constant" },
                { "\'([^\'\\\\]|\\\\.)*\'", "character_constant" },
                { "[0-9]*[\\.][0-9]+", "floating_constant" },
                { "[0-9]+", "integer_constant" },

                { "[a-zA-Z_][a-zA-Z0-9_]*", "identifier" },

                { "\\+", "add" },
                { "\\-", "sub" },
                { "\\*", "mul" },
                { "\\/", "div" },
                { "\\|", "orop" },
                { "&", "and" },
                { "\\^", "exor" },
                { ">>", "shr" },
                { "<<", "shl" },

                { "\\(", "lparen" },
                { "\\)", "rparen" },
                { "\\[", "lsqare" },
                { "\\]", "rsqare" },
                { "\\{", "rbracket" },
                { "\\}", "lbracket" },

                { "=", "assign" },

                { ";", "eos" },
                { "\\.", "dot" },
        };

        public Lexer(string code, List<string> keywords, Dictionary<string, string> tokens, string comment_sequence)
        {
            manager = new CodeManager(code: code);
            this.keywords = keywords;
            this.allowed_tokens = new SortedDictionary<string, string>(tokens, new LengthComparer());
            this.comment_sequence = comment_sequence;
        }

        public Lexer(string code)
        {
            manager = new CodeManager(code: code);
        }

        public List<Token> GetAllTokens()
        {
            Token t;
            List<Token> tokens = new List<Token>();
            while((t = getNextToken()).GetTokenType() != "eof")
            {
                tokens.Add(t);
            }

            return tokens;
        }

    }
}
