using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compiler.Lexer
{
    partial class Lexer
    {
        public Token getNextToken()
        {

            // Ignore whitespace and comments
            manager.Get("[ \t\r\v\n]+");
            manager.Get(comment_sequence);
            manager.Get("[ \t\r\n\n]+");

            string curtoken;
            foreach (KeyValuePair<string, string> token in allowed_tokens)
            {
                if((curtoken = manager.Get(token.Key)) != null)
                {
                    if (keywords.Contains(curtoken))
                    {
                        return new Token("keyword", curtoken);
                    }
                    return new Token(token.Value, curtoken);
                }
                curtoken = null;
            }

            if((curtoken = manager.Get(".")) != null)
            {
                return new Token("invalid", curtoken);
            }

            return new Token("eof", "EOF");
        }
    }
}
