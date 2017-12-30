using System.Collections.Generic;

namespace Lexer.Implementation
{
    /// <summary>
    /// A program to go through the input and get a list of the tokens in it
    /// </summary>
    public partial class RegexLexer
    {
        RegexCodeTokenizer manager;

        private readonly string comment_sequence = "/\\*([^*]|[\r\n]|(\\*+([^*/]|[\r\n])))*\\*+/";

        /// <summary>
        /// Default language keywords
        /// </summary>
        private readonly List<string> keywords = new List<string>()
        {
               "auto", "var", "int", "double",
               "entry", "global", "extern", "type",
               "if", "elif", "else", "while", "do",
               "import", "pass", "break", "undef",
               "exit", "string", "return", 
               
        };

        /// <summary>
        /// Default allowed tokens
        /// </summary>
        private readonly SortedDictionary<string, string> allowed_tokens = new SortedDictionary<string, string>(new LengthComparer())
            {
                { "[0-9]*[\\.][0-9]+", "float" },
                { "[0-9]+", "int" },

                { "[_a-zA-Z]\\w*", "ident"},

                { "\\+", "plus" },
               
                { "\\*", "mul" },
                { "\\/", "div" },
                { "\\|", "orop" },
                { "&", "and" },
                { "\\^", "exor" },
                { ">>", "shr" },
                { "<<", "shl" },
                { "\\~", "complement"},
                { "\\!", "not"},
                { "\\-", "minus"},

                { "[\\{\\[\\(\\)\\]\\}]", "divider"},

                { "=", "assign" },

                { ";", "eos" },
                { "\\.", "dot" },
        };

        /// <summary>
        /// Create a new lexer
        /// </summary>
        /// <param name="code">the code to be parsed</param>
        /// <param name="keywords">a list of language keywords</param>
        /// <param name="tokens">a list of regexes and token types to compare the code to</param>
        /// <param name="comment_sequence">the language sequence for ignorable comments</param>
        /// <param name="include_sequence">The language sequence for includation of other files</param>
        public RegexLexer(string code, List<string> keywords, Dictionary<string, string> tokens, 
            string comment_sequence, string include_sequence)
        {
            this.manager = new RegexCodeTokenizer(code: code);
            this.keywords = keywords;
            this.allowed_tokens = new SortedDictionary<string, string>(tokens, new LengthComparer());
            this.comment_sequence = comment_sequence;
        }

        /// <summary>
        /// A lexer that uses the default tokens to parse
        /// </summary>
        /// <param name="code">the code to be parsed</param>
        public RegexLexer(string code)
        {
            manager = new RegexCodeTokenizer(code: code);
        }

        /// <summary>
        /// Go through the input and make a list of tokens
        /// </summary>
        /// <returns>The list of tokens representing the input</returns>
        public List<Token> GetAllTokens()
        {
            Token t;
           List<Token> tokens = new List<Token>();
            while((t = GetNextToken()).GetTokenType() != "eof")
            {
                tokens.Add(t);
            }

            return tokens;
        }

    }
}
