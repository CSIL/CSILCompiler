using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LexerTests
{
    class Program
    {
        static int Main(string[] args)
        {

            Lexer.Implementation.Tests.RegexLexerTests tests = new Lexer.Implementation.Tests.RegexLexerTests();
            bool sucess = false;
            Console.WriteLine((sucess = tests.RunallTests()));
            return sucess ? 0 : 1;
        }
    }
}
