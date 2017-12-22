using Lexer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Implementation.Tests
{
    public class RegexLexerTests
    {
        public bool RegexLexerTest()
        {
            return true;
        }

        public bool RegexLexerTest1()
        {
            return true;
        }

        public bool GetAllTokensTest()
        {
            return true;
        }

        public bool RunallTests()
        {
            if (!RegexLexerTest()) return false;
            if (!RegexLexerTest1()) return false;
            if (!GetAllTokensTest()) return false;

            return true;
        }
    }
}